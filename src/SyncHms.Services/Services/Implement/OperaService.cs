namespace SyncHms.Services.Services.Implement;

internal class OperaService(IControl<OperaOptions, ApplicationEnvironment> control) : IOperaService
{
    private const string NameDataQuery =
        "SELECT hrs_dev.hrs_sh_sens.dob(n.name_id) AS BIRTHDAY, hrs_dev.hrs_sh_sens.pass_id(n.name_id) AS PASS_ID FROM opera.name n WHERE ROWNUM <= 1 AND n.name_id = {0}";

    private static readonly IReadOnlyDictionary<string, string> _sexAliases = new Dictionary<string, string>
    {
        { "1", "M" }, 
        { "2", "F" } 
    };
    private OperaDbContext Context => new(control.Options.ConnectionString, control.Options.OracleSqlCompatibility);

    public ApplicationEnvironment Environment => control.Environment;

    public async Task ChangedOptionsHandleAsync(OperaOptions options) 
    {
        await using var context = Context;
        await Context.ReservationNames.AsNoTracking().AnyAsync();
    }

    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;

    public async Task<ReservationUpdatedMessage?> GetReservationUpdatedMessageAsync(decimal reservationId, string status,
        string? room, DateTime arrival, DateTime departure)
    {
        try
        {
            await using var context = Context;
            var reservationResponse = await (from rn in context.ReservationNames
                from n in context.Names
                where rn.Resort == Environment.ResortCode && rn.ResvNameId == reservationId &&
                      n.NameId == rn.NameId
                select new
                {
                    //CustomFieldValues = 
                    GuestGenericNo = $"{n.NameId:0}",
                    Id = $"{n.NameId:0}/{reservationId:0}",
                    FirstName = Trim(n.XfirstName ?? n.First),
                    LastName = Trim(n.XlastName ?? n.Last),
                    MiddleName = Trim(n.XmiddleName ?? n.Middle),
                    Sex = n.Gender,
                    BirthDateStr = string.Empty,
                    BirthDate = DateTime.Now,
                    //Notes = 
                    rn.TruncBeginDate,
                    rn.TruncEndDate,
                    DocumentTypeCode = (from nd in context.NameDocuments
                        where nd.PrimaryYn == "Y" && nd.NameId == n.NameId
                        select nd.IdType).AsNoTracking().FirstOrDefault(),
                    //DocumentTypeName =
                    //DocumentNumber = 
                    DocumentSeries = n.Udfc01,
                    DepartmentCode = n.Udfc03,
                    IssueDate = ToDateTime(n.Udfc02, "dd.MM.yy"),
                    //ExpirationDate = 
                    //RegistrationDate = 
                    IssuerInfo = Trim(n.Udfc04),
                    Timelines = (from rden in context.ReservationDailyElementNames
                        from rde in context.ReservationDailyElements
                        where rden.Resort == rn.Resort && rden.ResvNameId == rn.ResvNameId &&
                              rde.Resort == rden.Resort && rde.ResvDailyElSeq == rden.ResvDailyElSeq &&
                              rde.ReservationDate == rden.ReservationDate
                        select new
                        {
                            DateTimeFrom = rden.ReservationDate ?? default,
                            DateTimeTo = (rden.ReservationDate ?? default).AddDays(1).AddTicks(-1),
                            EffectiveDate = rden.ReservationDate,
                            StayPriceLocalCurrencyAmount = CalcShareAmount(rden.ShareAmount, rden.SharePrcnt),
                            RoomTypeCode = (from rrc in context.ResortRoomCategories
                                where rrc.Resort == rde.Resort && rrc.RoomCategory == rde.RoomCategory
                                select rrc.Label).AsNoTracking().FirstOrDefault(),
                            RateName = rden.RateCode,
                            RoomCode = rde.Room,
                            Packages = (from rp in context.ReservationProducts
                                from rpp in context.ReservationProductPrices
                                from ppr in context.ProductPostingRules
                                where rp.Resort == rden.Resort && rp.ResvNameId == rden.ResvNameId &&
                                      rpp.Resort == rp.Resort && rpp.ReservationDate == rden.ReservationDate &&
                                      rpp.ReservationProductId == rp.ReservationProductId && ppr.Resort == rpp.Resort &&
                                      ppr.Product == rp.ProductId && ppr.TrxCode != null && Environment.TrxCodes.Contains(ppr.TrxCode)
                                select new
                                {
                                    Code = rp.ProductId,
                                    Amount = rpp.Price ?? default,
                                    Count = (int?)rpp.Quantity,
                                    rp.CurrencyCode
                                }).AsNoTracking().ToHashSet()
                        }).AsNoTracking().ToHashSet(),
                    BusinnesDate = (from b in context.Businessdates
                        where b.Resort == rn.Resort && b.State == "OPEN"
                        select b.BusinessDate1).FirstOrDefault(),
                    Address = (from na in context.NameAddresses
                        from c in context.Countries
                        where na.NameId == n.NameId && na.PrimaryYn == "Y" && na.InactiveDate == null
                              && c.CountryCode == na.Country
                        select new
                        {
                            CountryCode = c.IsoCode,
                            Region = Trim(na.State),
                            City = Trim(na.City),
                            Street = JoinAddress(na.Address1, na.Address2, na.Address3, na.Address4)
                        }).AsNoTracking().FirstOrDefault(),
                    Phones = (from np in context.NamePhones
                        where np.NameId == n.NameId
                        select new { np.PhoneNumber, np.PhoneType }).AsNoTracking().ToHashSet()
                }).AsNoTracking().FirstOrDefaultAsync();

            if (reservationResponse == null)
                return null;
            
            var nameInfo = await context.NameData
                .FromSqlRaw(string.Format(NameDataQuery, reservationResponse.Id))
                .FirstOrDefaultAsync();
            
            var message = new ReservationUpdatedMessage()
            {
                GenericNo = reservationId.ToString("0"),
                Status = status,
                ArrivalDate = arrival,
                DepartureDate = departure,
                //CustomFieldValues = 
                ReservationGuests =
                [
                    new Guest
                    {
                        GenericNo = reservationResponse.GuestGenericNo,
                        Id = reservationResponse.Id,
                        FirstName = reservationResponse.FirstName,
                        LastName = reservationResponse.LastName,
                        MiddleName = reservationResponse.MiddleName,
                        Sex = FixSex(reservationResponse.Sex),
                        BirthDate = ToDateTime(nameInfo?.BirthDay, "dd.MM.yyyy"),
                        CountryCode = reservationResponse.Address?.CountryCode,
                        Region = reservationResponse.Address?.Region,
                        City = reservationResponse.Address?.City,
                        Street = reservationResponse.Address?.Street,
                        //Notes = 
                        Phones = reservationResponse.Phones.Select(p => new GuestPhone
                        {
                            PhoneNumber = p.PhoneNumber,
                            PhoneType = p.PhoneType
                        }).ToArray(),
                        DocumentData = new DocumentData
                        {
                            DocumentTypeCode = FixDocumentTypeCode(reservationResponse.DocumentTypeCode),
                            //DocumentTypeName =
                            DocumentSeries = reservationResponse.DocumentSeries,
                            DocumentNumber = nameInfo?.PassId,
                            IssueDate = reservationResponse.IssueDate,
                            //ExpirationDate = 
                            DepartmentCode = reservationResponse.DepartmentCode,
                            IssuerInfo = reservationResponse.IssuerInfo
                        }
                    }
                ]
            };

            if (reservationResponse.Timelines.Count == 0)
            {
                var timeline = new ReservationMessage.Timeline
                {
                    DateRange = new DateRange
                    {
                        DateTimeFrom = arrival,
                        DateTimeTo = departure
                    },
                    EffectiveDate = arrival,
                    RoomCode = room
                };

                message.Timelines = [timeline];
                message.CurrentTimeline = timeline;
            }
            else
            {
                message.Timelines = reservationResponse.Timelines.Select(t => new ReservationMessage.Timeline
                {
                    DateRange = new DateRange
                    {
                        DateTimeFrom = t.DateTimeFrom,
                        DateTimeTo = t.DateTimeTo
                    },
                    EffectiveDate = t.EffectiveDate,
                    StayPriceLocalCurrencyAmount = t.StayPriceLocalCurrencyAmount,
                    RoomTypeCode = t.RoomTypeCode,
                    RateName = t.RateName,
                    RoomCode = t.RoomCode,
                    Packages = t.Packages.Select(p => new Package
                    {
                        Code = p.Code,
                        Amount = p.Amount,
                        Count = p.Count,
                        CurrencyCode = p.CurrencyCode
                    }).ToArray()
                }).ToArray();

                if (reservationResponse.TruncBeginDate != reservationResponse.TruncEndDate)
                    message.Timelines = message.Timelines
                        .Where(t => t.DateRange.DateTimeFrom != reservationResponse.TruncEndDate)
                        .ToArray();
                
                message.CurrentTimeline = 
                    message.Timelines.FirstOrDefault(t =>
                        reservationResponse.BusinnesDate != null &&
                        t.DateRange.DateTimeFrom == reservationResponse.BusinnesDate)
                    ?? message.Timelines.First();
            }
            
            control.Active();
            return message;
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
    }

    private string? FixDocumentTypeCode(string? value)
    {
        if (value == null)
            return null;

        if (Environment.DocumentTypeAliases.Count == 0)
            return value;

        foreach (var alias in Environment.DocumentTypeAliases)
            if (value == alias.Key)
                return alias.Value;

        return value;
    }

    private static string? FixSex(string? value)
    {
        if (value == null)
            return null;

        foreach (var alias in _sexAliases)
            if (value == alias.Key)
                return alias.Value;

        return value;
    }

    private static DateTime? ToDateTime(string? value, string format) 
        => value == null || !DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out DateTime issue)
            ? null : issue;

    private static string? Trim(string? value) => value?.Trim();

    private static decimal CalcShareAmount(decimal? shareAmount, decimal? sharePrcnt) => (shareAmount ?? 0) * (sharePrcnt ?? 100) / 100;

    private static string? JoinAddress(params string?[]? addresses)
    {
        if (addresses == null)
            return null;

        var result = new List<string>(addresses.Length);

        foreach (var address in addresses)
        {
            var temp = address?.Trim();
            if (!string.IsNullOrEmpty(temp))
                result.Add(temp);
        }

        return string.Join(", ", result);
    }
}

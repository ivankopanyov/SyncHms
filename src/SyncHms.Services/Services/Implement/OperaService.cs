namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с базой данных <c>OPERA</c><br/>
/// Реализует интерфейс <see cref="IOperaService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class OperaService(IControl<OperaOptions, ApplicationEnvironment> control) : IOperaService
{
    /// <summary>
    /// Запрос к базе данных <c>ORACLE</c>, вызывающий процедуры
    /// <c>hrs_dev.hrs_sh_sens.dob</c> и <c>hrs_dev.hrs_sh_sens.pass_id</c>,
    /// возвращающие дату рождения и номер паспорта соответственно.
    /// </summary>
    private const string NameDataQuery =
        "SELECT hrs_dev.hrs_sh_sens.dob(n.name_id) AS BIRTHDAY, hrs_dev.hrs_sh_sens.pass_id(n.name_id) AS PASS_ID FROM opera.name n WHERE ROWNUM <= 1 AND n.name_id = {0}";

    /// <summary>Словарь соответсвия обозначения полов с системах <c>OPERA</c> и <c>Sanatorium</c></summary>
    private static readonly IReadOnlyDictionary<string, string> SexAliases = new Dictionary<string, string>
    {
        { "1", "M" }, 
        { "2", "F" } 
    };
    
    /// <summary>Экземпляр опций транзакции.</summary>
    private static readonly TransactionOptions TransactionOptions = new()
    {
        IsolationLevel = IsolationLevel.ReadCommitted
    };

    /// <summary>Свойство, возвращающее новый экземпляр контекста для подключения к базе данных.</summary>
    private OperaDbContext Context => new(control.Options.ConnectionString, control.Options.OracleSqlCompatibility);

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;
    
    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public async Task ChangedOptionsHandleAsync(OperaOptions options) 
    {
        using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress, TransactionOptions))
        {
            await using var context = Context;
            await context.ReservationNames.AsNoTracking().AnyAsync();
        }
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;
    
    /// <summary>Метод, запрашивающий номер комнаты бронирования в базе данных <c>OPERA</c></summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <returns>Номер комнаты бронирования.</returns>
    public async Task<string?> GetRoomNumberAsync(decimal reservationId)
    {
        string? result = null;

        try
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress, TransactionOptions))
            {
                await using var context = Context;
                result = await (from rn in context.ReservationNames
                                from rdn in context.ReservationDailyElementNames
                                from rde in context.ReservationDailyElements
                                from b in context.Businessdates
                                where rn.Resort == Environment.ResortCode && rn.ResvNameId == reservationId &&
                                        rn.Resort == rdn.Resort && rn.ResvNameId == rdn.ResvNameId &&
                                        b.Resort == rn.Resort && b.State == "OPEN" && rdn.Resort == rde.Resort &&
                                        rdn.ResvDailyElSeq == rde.ResvDailyElSeq && rdn.ReservationDate == rde.ReservationDate
                                        && rdn.ReservationDate == b.BusinessDate1
                                select rde.Room).FirstOrDefaultAsync();
            }

            control.Active();
            return result;
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
    }

    /// <summary>Метод, запрашивающий данные бронирования в базе данных <c>OPERA</c></summary>
    /// <param name="reservationId">Идентификатор бронирования.</param>
    /// <param name="status">Статус бронирования.</param>
    /// <returns>Данные бронирования.</returns>
    public async Task<ReservationUpdatedMessage?> GetReservationUpdatedMessageAsync(decimal reservationId, string status)
    {
        ReservationUpdatedMessage? message = null;

        try
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress, TransactionOptions))
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
                                                     Arrival = rn.ActualCheckInDate != null,
                                                     Departure = rn.ActualCheckOutDate != null,
                                                     rn.BeginDate,
                                                     rn.EndDate,
                                                     rn.PostingAllowedYn,
                                                     rn.InsertDate,
                                                     rn.UpdateDate,
                                                     DocumentTypeCode = (from nd in context.NameDocuments
                                                                         where nd.PrimaryYn == "Y" && nd.NameId == n.NameId
                                                                         select nd.IdType).FirstOrDefault(),
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
                                                                                      select rrc.Label).FirstOrDefault(),
                                                                      rden.RateCode,
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
                                                                                  }).ToHashSet()
                                                                  }).ToHashSet(),
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
                                                                }).FirstOrDefault(),
                                                     Phones = (from np in context.NamePhones
                                                               where np.NameId == n.NameId
                                                               select new { np.PhoneNumber, np.PhoneType }).ToHashSet()
                                                 }).FirstOrDefaultAsync();

                if (reservationResponse != null)
                {
                    var nameInfo = await context.NameData
                        .FromSqlRaw(string.Format(NameDataQuery, reservationResponse.GuestGenericNo))
                        .FirstOrDefaultAsync();

                    var noPost = reservationResponse.PostingAllowedYn != "Y";
                    var notes = GetNote(reservationResponse.Timelines.SelectMany(t => t.Packages).Select(p => p.Code).ToHashSet(), noPost);

                    CustomField[]? customFields = !string.IsNullOrWhiteSpace(Environment.SanatoriumCustomField)
                        ? [
                            new CustomField
                            {
                                Code = Environment.SanatoriumCustomField,
                                StringValue = notes
                            }]
                        : null;

                    var arrivalDate = reservationResponse.Arrival ? reservationResponse.BeginDate : null;
                    var departureDate = reservationResponse.Departure ? reservationResponse.EndDate : null;

                    message = new ReservationUpdatedMessage
                    {
                        GenericNo = reservationId.ToString("0"),
                        Status = status,
                        ArrivalDate = reservationResponse.BeginDate ?? default,
                        DepartureDate = reservationResponse.EndDate ?? default,
                        ActualArrivalDate = arrivalDate,
                        ActualDepartureDate = departureDate,
                        FactArrivalDateTime = arrivalDate,
                        FactDepartureDateTime = departureDate,
                        CreatedDate = reservationResponse.InsertDate ?? default,
                        ModifiedDate = reservationResponse.UpdateDate ?? default,
                        CustomFieldValues = customFields!,
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
                                Notes = notes,
                                NoPost = noPost,
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

                    if (reservationResponse.Timelines.Count > 0)
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
                            RateCode = t.RateCode,
                            RoomCode = t.RoomCode,
                            Packages = t.Packages.Select(p => new Package
                            {
                                Code = p.Code,
                                Amount = p.Amount,
                                Count = p.Count,
                                CurrencyCode = p.CurrencyCode
                            }).ToArray()
                        }).ToArray();

                        var currentTimeline = message.Timelines.FirstOrDefault(t =>
                            reservationResponse.BusinnesDate != null &&
                            t.DateRange.DateTimeFrom == reservationResponse.BusinnesDate);

                        if (currentTimeline != null)
                            message.CurrentTimeline = currentTimeline;
                    }
                }
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

    /// <summary>Метод, возвращающий коллекцию бронирований, которые были обновлены ы указанный период.</summary>
    /// <param name="fromDate">Минимальная дата обновления бронирования.</param>
    /// <param name="toDate">Максимальная дата обноаления бронирования.</param>
    /// <returns>Коллекция обновленных бронирований.</returns>
    public async Task<List<UpdatedReservation>> GetUpdatedReservationsAsync(DateTime fromDate, DateTime toDate)
    {
        List<UpdatedReservation> result;

        try
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Suppress, TransactionOptions))
            {
                var context = Context;
                result = await (from rn in context.ReservationNames
                    where rn.Resort == Environment.ResortCode && rn.ResvNameId != null &&
                          rn.UpdateDate > fromDate && rn.UpdateDate <= toDate && rn.ResvStatus != null
                    select new UpdatedReservation
                    {
                        ReservationNumber = (decimal)rn.ResvNameId!,
                        Status = rn.ResvStatus
                    }).ToListAsync();
            }

            control.Active();
            return result;
        }
        catch (Exception ex)
        {
            control.Unactive(ex);
            throw;
        }
    }

    /// <summary>Метод, формирующий текст заметки.</summary>
    /// <param name="codes">Коды пакетов бронирования.</param>
    /// <param name="noPost">Статус возможности начисления платежей на комнату.</param>
    /// <returns>Текст заметки.</returns>
    private static string GetNote(IEnumerable<string> codes, bool? noPost)
    {
        var note = string.Join(", ", codes);
        if (noPost == true)
            note += ". NO_POST";

        return note;
    }
    
    /// <summary>Метод, определяющий код документа по псевдониму.</summary>
    /// <param name="value">Псевдоним кода документа.</param>
    /// <returns>
    /// Код документа.<br/>
    /// Если передано значение параметра <c>null</c>
    /// или код не найден, вернет <c>null</c>
    /// </returns>
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
    
    /// <summary>Метод, преобразующий код пола <c>OPERA</c> в код пола <c>Sanatorium</c></summary>
    /// <param name="value">Значение пола.</param>
    /// <returns>
    /// Результат преобразования.<br/>
    /// Если переданный код не найден,
    /// будет возвращено значение параметра <c>value</c>
    /// </returns>
    private static string? FixSex(string? value)
    {
        if (value == null)
            return null;

        foreach (var alias in SexAliases)
            if (value == alias.Key)
                return alias.Value;

        return value;
    }
    
    /// <summary>Метод, преобразующий строку в <see cref="DateTime"/></summary>
    /// <param name="value">Строка для преобразования.</param>
    /// <param name="format">Формат даты.</param>
    /// <returns>
    /// Результат преобразования.<br/>
    /// Если преобразование не удалось, будет возвращен <c>null</c>
    /// </returns>
    private static DateTime? ToDateTime(string? value, string format) 
        => value == null || !DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out DateTime issue)
            ? null : issue;

    /// <summary>Метод, удаляющий пробелы в начале и конце строки.</summary>
    /// <param name="value">Строка для преобразования.</param>
    /// <returns>
    /// Итоговая строка.<br/>
    /// Если передан <c>null</c> - будет возвращен <c>null</c>
    /// </returns>
    private static string? Trim(string? value) => value?.Trim();

    /// <summary>Метод, рассчитывающий сумму в учетом скидки.</summary>
    /// <param name="shareAmount">Сумма до скидки.</param>
    /// <param name="sharePrcnt">Процент скидки.</param>
    /// <returns>Сумма со скидкой.</returns>
    private static decimal CalcShareAmount(decimal? shareAmount, decimal? sharePrcnt)
    {
        return (shareAmount ?? 0) * (sharePrcnt ?? 100) / 100;
    }

    /// <summary>Метод, объединяющий элементы адреса в строку.</summary>
    /// <param name="addresses">Элементы адреса.</param>
    /// <returns>Адрес.</returns>
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

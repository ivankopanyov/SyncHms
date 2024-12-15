namespace SyncHms.Services.Services.Implement;

/// <summary>
/// Класс, описывающий сервис интеграции с базой данных <c>OzLocks</c><br/>
/// Реализует интерфейс <see cref="IOzLocksService"/>
/// </summary>
/// <param name="control">Экземпляр контроллера, управляющего состоянием сервиса.</param>
internal class OzLocksService(IControl<OzLocksOptions, ApplicationEnvironment> control) : IOzLocksService
{
    /// <summary>Свойство, возвращающее новый экземпляр контекста для подключения к базе данных.</summary>
    private OzLocksContext Context => new(control.Options.ConnectionString);

    /// <summary>Экземпляр окружения.</summary>
    public ApplicationEnvironment Environment => control.Environment;

    /// <summary>Метод, обрабатывающий изменение опций сервиса.</summary>
    /// <param name="options">Экземпляр опций сервиса.</param>
    public async Task ChangedOptionsHandleAsync(OzLocksOptions options)
    {
        await using var context = Context;
        await context.Guests.AsNoTracking().AnyAsync();
    }

    /// <summary>Метод, обрабатывающий изменение значений переменных окружения.</summary>
    /// <param name="current">Текущее окружение.</param>
    /// <param name="previous">Измененное окружение.</param>
    public Task ChangedEnvironmentHandleAsync(ApplicationEnvironment current, ApplicationEnvironment previous) => Task.CompletedTask;

    public async Task<HashSet<InventoryStatus>> UpdateInventoriesAsync(ReservationInventory reservationInventory)
    {
        List<AuditUser> auditUsers = [];

        var inventoryCodes = reservationInventory.Inventories
            .Select(i => i.ArticleNumber)
            .ToHashSet();

        await using var context = Context;
        using var transaction = context.Database.BeginTransaction();

        var checkIn = await CheckInAsync(reservationInventory, context, inventoryCodes, auditUsers);
        var checkOut = await CheckOutAsync(reservationInventory, context, inventoryCodes, auditUsers);

        await context.AuditUsers.AddRangeAsync(auditUsers);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();

        return [.. checkIn, .. checkOut];
    }

    private async Task<HashSet<InventoryStatus>> CheckInAsync(ReservationInventory reservationInventory, OzLocksContext context,
        HashSet<string> inventoryCodes, List<AuditUser> auditUsers)
    {
        HashSet<InventoryStatus> result = [];
        HashSet<string> longest = [];

        var current = await (from r in context.Rooms
                             from ra in context.RoomActions.Where(ra => ra.ActionId == r.CurActionId).DefaultIfEmpty()
                             from g in context.Guests.Where(g => g.Foreigner == reservationInventory.ReservationId
                                && g.ActionId == ra.ActionId && g.InRoom == true && g.DeleteMark == 0).DefaultIfEmpty()
                             where inventoryCodes.Contains(r.FidelioId)
                             select new { Room = r, RoomAction = ra, Guest = g })
                             .ToListAsync();

        var now = DateTime.Now;

        foreach (var item in current)
        {
            var inv = reservationInventory.Inventories.First(i => i.ArticleNumber == item.Room.FidelioId);
            var duration = (int)double.Ceiling((inv.EndDate - inv.BeginDate).TotalDays);

            bool isCheckIn = false, isRenewal = false;

            if (item.RoomAction is not { } roomAction)
            {
                roomAction = new RoomAction
                {
                    RoomId = item.Room.RoomId,
                    TimeIn = inv.BeginDate,
                    TimeOut = inv.EndDate,
                    Duration = duration,
                    TimeOutS = inv.EndDate,
                    DayS = duration,
                    AuserId = control.Options.IntegrationUserId,
                    TimeStamp = now
                };

                await context.RoomActions.AddAsync(roomAction);
                await context.SaveChangesAsync();

                auditUsers.Add(new AuditUser
                {
                    CatId = 9,
                    ActId = 44,
                    UserId = control.Options.IntegrationUserId,
                    ObjId = item.Room.RoomId
                });

                item.Room.CurActionId = roomAction.ActionId;
            }
            else if (roomAction.TimeIn != inv.BeginDate || roomAction.TimeOut != inv.EndDate)
            {
                isRenewal = true;

                roomAction.TimeIn = inv.BeginDate;
                roomAction.TimeOut = inv.EndDate;
                roomAction.Duration = duration;
                roomAction.TimeOutS = inv.EndDate;
                roomAction.DayS = duration;

                auditUsers.Add(new AuditUser
                {
                    CatId = 9,
                    ActId = 45,
                    UserId = control.Options.IntegrationUserId,
                    ObjId = item.Room.RoomId
                });
            }

            if (item.Guest is not { } guest)
            {
                isRenewal = false;
                isCheckIn = true;

                item.Room.RoomGuestCount++;

                guest = new OzLocks.Entities.Guest
                {
                    Foreigner = (int)reservationInventory.ReservationId,
                    Name = reservationInventory.FirstName,
                    Fam = reservationInventory.LastName,
                    Otch = reservationInventory.MiddleName,
                    Sex = reservationInventory.Sex,
                    RoomId = item.Room.RoomId,
                    Phone = $"{reservationInventory.ConfirmationNo}/{reservationInventory.Room}",
                    ActionId = roomAction.ActionId,
                    GuserId = control.Options.IntegrationUserId
                };

                await context.Guests.AddAsync(guest);
                await context.SaveChangesAsync();

                auditUsers.Add(new AuditUser
                {
                    CatId = 13,
                    ActId = 61,
                    UserId = control.Options.IntegrationUserId,
                    ObjId = guest.GuestId
                });
            }

            if (isCheckIn)
                result.Add(new(OzLocksStatus.CheckIn, item.Room.RoomName ?? item.Room.RoomNum?.ToString() ?? item.Room.RoomId.ToString()));

            if (isRenewal)
                result.Add(new(OzLocksStatus.Change, item.Room.RoomName ?? item.Room.RoomNum?.ToString() ?? item.Room.RoomId.ToString()));
        }

        return result;
    }

    private async Task<HashSet<InventoryStatus>> CheckOutAsync(ReservationInventory reservationInventory, OzLocksContext context,
        HashSet<string> inventoryCodes, List<AuditUser> auditUsers)
    {
        HashSet<InventoryStatus> result = [];

        var current = await (from r in context.Rooms
                             join ra in context.RoomActions on r.CurActionId equals ra.ActionId
                             join g in context.Guests
                                  on new { Foreigner = (int?)reservationInventory.ReservationId, ActionId = (int?)ra.ActionId, InRoom = (bool?)true, DeleteMark = 0 }
                                  equals new { g.Foreigner, g.ActionId, g.InRoom, g.DeleteMark }
                             where !inventoryCodes.Contains(r.FidelioId)
                             select new { Room = r, RoomAction = ra, Guest = g })
                    .ToListAsync();

        var now = DateTime.Now;

        foreach (var item in current)
        {
            result.Add(new(OzLocksStatus.CheckOut, item.Room.RoomName));

            item.Guest.InRoom = false;

            auditUsers.Add(new AuditUser
            {
                CatId = 10,
                ActId = 47,
                UserId = control.Options.IntegrationUserId,
                ObjId = item.Guest.RoomId
            });

            item.Guest.DeleteMark = 1;

            auditUsers.Add(new AuditUser
            {
                CatId = 13,
                ActId = 62,
                UserId = control.Options.IntegrationUserId,
                ObjId = item.Guest.GuestId
            });

            item.Room.FactTimeOut = now;

            if (--item.Room.RoomGuestCount == 0)
            {
                item.RoomAction.FactTimeOut = now;
                item.Room.CurActionId = 0;
            }
        }

        return result;
    }
}

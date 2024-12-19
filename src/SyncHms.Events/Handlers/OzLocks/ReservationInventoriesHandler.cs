namespace SyncHms.Events.Handlers.OzLocks;

/// <summary>
/// Класс, описывающий обработчик события <see cref="ReservationInventories"/>,
/// обновляющий данные о резервировании бронирования в базе данных <c>OzLocks</c>.<br/>
/// Унаследован от класса <see cref="Handler{TIn}"/>
/// </summary>
internal class ReservationInventoriesHandler(IOzLocksService ozLocksService) : Handler<ReservationInventories>
{
    /// <summary>
    /// Метод, обрабатывающий событие <see cref="ReservationInventories"/>.
    /// Проводит попытку обновления статуса резервирования инвентаря в базе данных <c>OzLocks</c>.<br/>
    /// Переопределяет метод <see cref="Handler{TIn}.HandleAsync"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <param name="context">Контекст обработки события.</param>
    protected override async Task HandleAsync(ReservationInventories @in, IEventContext context)
    {
        var reservationInventories = new SortedSet<ReservationInventory>(@in.Queue);
        if (await GetFirstNotEmptyReservationInventoryAsync(reservationInventories) is not { } reservationInventory)
        {
            context.Logiable = @in.HasInventories || @in.HasError || context.HasError;
            return;
        }

        @in.HasInventories = true;

        var inventories = new SortedSet<InventoryStatus>(reservationInventory.InventoryQueue!);
        var firstInventory = inventories.First();

        SetHandlerName(firstInventory.Status, context);
        SetMessage(reservationInventory, firstInventory, context);

        inventories.Remove(firstInventory);

        if (inventories.Count == 0)
        {
            reservationInventories.Remove(reservationInventory);
            if (reservationInventories.Count == 0)
                return;
        }
        else
        {
            reservationInventory.InventoryQueue = [.. inventories];
        }

        if (await GetFirstNotEmptyReservationInventoryAsync(reservationInventories) == null)
            return;

        @in.Queue = [.. reservationInventories];
        context.Send(@in);
    }

    /// <summary>
    /// Метод, возвращающий краткое описание события <see cref="ReservationInventories"/><br/>
    /// Переопределяет метод <see cref="Handler{TIn}.Message"/>
    /// </summary>
    /// <param name="in">Экземпляр обрабатываемого события.</param>
    /// <returns>Краткое описание события.</returns>
    protected override string? Message(ReservationInventories @in)
    {
        return @in.Queue.FirstOrDefault()?.Room is { } room ? $"Room: {room}" : null;
    }

    /// <summary>Метод переопределяет имя обработчика <see cref="ReservationInventoriesHandler"/> в логах.</summary>
    /// <param name="inventoryStatus">Экземпляр обновленного инвентаря.</param>
    /// <param name="context">Контекст обработки события.</param>
    private static void SetHandlerName(OzLocksStatus inventoryStatus, IEventContext context)
    {
        var handlerName = inventoryStatus switch
        {
            OzLocksStatus.CheckOut => "CHECK_OUT",
            OzLocksStatus.Change => "CHANGE",
            OzLocksStatus.CheckIn => "CHECK_IN",
            OzLocksStatus.Update => "UPDATE",
           _ => "UNKNOWN"
        };

        context.SetHandlerName(handlerName);
    }

    /// <summary>Метод переопределяет сообщение обработчика <see cref="ReservationInventoriesHandler"/> в логах.</summary>
    /// <param name="inventory">Экземпляр обрабатываемого бронирования.</param>
    /// <param name="inventoryStatus">Экземпляр обновленного инвентаря.</param>
    /// <param name="context">Контекст обработки события.</param>
    private static void SetMessage(ReservationInventory inventory, InventoryStatus inventoryStatus, IEventContext context)
    {
        var stringBuilder = new StringBuilder();

        if (inventoryStatus.Status != OzLocksStatus.Change)
            stringBuilder.Append($"{inventory.LastName} {inventory.FirstName} {inventory.MiddleName}, Reservation: {inventory.ReservationId:0}, ");

        stringBuilder.Append($"Locker: {inventoryStatus.InventoryCode}");

        if (!string.IsNullOrWhiteSpace(inventory.Room))
            stringBuilder.Append($", Room: {inventory.Room}");

        context.SetMessage(stringBuilder.ToString());
    }

    /// <summary>
    /// Метод возвращает первое бронирование, которое содержит изменение в резервировании инвентаря.<br/>
    /// Если бронирование не найдено, возвращает <c>null</c>
    /// </summary>
    /// <param name="reservationInventories">Экземпляр коллекции бронирований.</param>
    /// <returns>Первое бронирование, которое содержит изменение в резервировании инвентаря.</returns>
    private async Task<ReservationInventory?> GetFirstNotEmptyReservationInventoryAsync(SortedSet<ReservationInventory> reservationInventories)
    {
        ReservationInventory reservationInventory;

        do
        {
            reservationInventory = reservationInventories.First();
            reservationInventory.InventoryQueue ??= await ozLocksService.UpdateInventoriesAsync(reservationInventory);
            if (reservationInventory.InventoryQueue.Count == 0)
                reservationInventories.Remove(reservationInventory);

            if (reservationInventories.Count == 0)
                return null;
        }
        while (reservationInventory.InventoryQueue.Count == 0);

        return reservationInventory;
    }
}

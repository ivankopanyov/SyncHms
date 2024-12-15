namespace SyncHms.Events.Handlers.Ozlocks;

internal class ReservationInventoriesHandler(IOzLocksService ozLocksService) : Handler<ReservationInventories>
{
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

    protected override string? Message(ReservationInventories @in)
    {
        return @in.Queue.FirstOrDefault()?.Room is { } room ? $"Room: {room}" : null;
    }

    private static void SetHandlerName(OzLocksStatus inventoryStatus, IEventContext context)
    {
        var handlerName = inventoryStatus switch
        {
            OzLocksStatus.CheckOut => "CHECK_OUT",
            OzLocksStatus.Change => "CHANGE",
            OzLocksStatus.CheckIn => "CHECK_IN",
           _ => "UNKNOWN"
        };

        context.SetHandlerName(handlerName);
    }

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

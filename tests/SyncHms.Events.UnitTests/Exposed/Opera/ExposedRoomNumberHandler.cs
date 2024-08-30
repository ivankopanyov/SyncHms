namespace SyncHms.Events.UnitTests.Exposed.Opera;

internal class ExposedRoomNumberHandler(IOperaService operaService) : RoomNumberHandler(operaService)
{
    public Task ExposedHandleAsync(RoomNumberRequest @in, IEventContext context)
    {
        return base.HandleAsync(@in, context);
    }
}

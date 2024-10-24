namespace SyncHms.Events.UnitTests.Cases.Fias;

internal class CheckOutHandlerCases : IEnumerable<object[]>
{
    private static readonly int?[] Source = [null, Random.Shared.Next()];

    public IEnumerator<object[]> GetEnumerator()
    {
        return Source.Select(item =>
        {
            var reservationNumber = Random.Shared.Next();
            var roomNumber = item?.ToString();
            
            return new object[]
            {
                new FiasGuestCheckOut
                {
                    ReservationNumber = reservationNumber,
                    RoomNumber = roomNumber
                },
                new ReservationInfo
                {
                    ReservationNumber = reservationNumber,
                    Status = SanatoriumReservationStatus.CheckedOut
                }
            };
        }).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
namespace SyncHms.Events.UnitTests.Cases.Fias;

internal class CheckInHandlerCases : IEnumerable<object[]>
{
    private static readonly DateTime Now = DateTime.Now;
    
    private static readonly
        (DateOnly? Arrival, DateOnly? Departure, DateTime? ExceptedArrival, DateTime? ExceptedDeparture)[] Source = [
        (null, null, null, null),
        (DateOnly.FromDateTime(Now),
            DateOnly.FromDateTime(Now.AddDays(3)),
            new DateTime(Now.Year, Now.Month, Now.Day),
            new DateTime(Now.Year, Now.Month, Now.Day).AddDays(3)),
        (DateOnly.FromDateTime(Now),
            DateOnly.FromDateTime(Now),
            new DateTime(Now.Year, Now.Month, Now.Day),
            new DateTime(Now.Year, Now.Month, Now.Day).AddDays(1).AddTicks(-1)),
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        return Source.Select((item, index) =>
        {
            var reservationNumber = Random.Shared.Next();
            var roomNumber = item.Arrival != null ? Random.Shared.Next().ToString() : null;
            bool? noPost = index == 0 ? null : index % 2 == 0;
            
            return new object[]
            {
                new FiasGuestCheckIn
                {
                    ReservationNumber = reservationNumber,
                    RoomNumber = roomNumber,
                    GuestArrivalDate = item.Arrival,
                    GuestDepartureDate = item.Departure,
                    NoPostStatus = noPost
                },
                new ReservationInfo
                {
                    ReservationNumber = reservationNumber,
                    Room = roomNumber,
                    Status = "IN",
                    ArrivalDate = item.ExceptedArrival,
                    DepartureDate = item.ExceptedDeparture,
                    NoPost = noPost
                }
            };
        }).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
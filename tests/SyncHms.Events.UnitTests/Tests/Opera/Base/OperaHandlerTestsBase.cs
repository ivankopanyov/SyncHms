using Moq;

namespace SyncHms.Events.UnitTests.Tests.Opera.Base;

public abstract class OperaHandlerTestsBase
{
    private protected static IOperaService GetOperaService(string resortCode, bool returnNull = false,
        IEnumerable<string>? statuses = null, decimal[]? reservationNumbers = null)
    {
        var operaService = new Mock<IOperaService>();

        operaService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            ResortCode = resortCode
        });

        operaService
            .Setup(fs => fs.GetReservationUpdatedMessageAsync(It.IsAny<decimal>(), It.IsAny<string>()))
            .ReturnsAsync(returnNull ? null : new ReservationUpdatedMessage());

        operaService
            .Setup(fs => fs.GetUpdatedReservationsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync((statuses ?? []).Select(i => new UpdatedReservation
            {
                ReservationNumber = Random.Shared.Next(),
                Status = i
            }).ToList());

        operaService
            .Setup(fs => fs.GetReservationsByProfileAsync(It.IsAny<decimal>(), It.IsAny<string?>()))
            .ReturnsAsync((reservationNumbers ?? []).ToList());

        return operaService.Object;
    }

    private protected static IOperaService GetOperaServiceWithThrow()
    {
        var operaService = new Mock<IOperaService>();

        operaService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            ResortCode = "ResortCode"
        });

        operaService
            .Setup(fs => fs.GetReservationUpdatedMessageAsync(It.IsAny<decimal>(), It.IsAny<string>()))
            .Throws<Exception>();

        operaService
            .Setup(fs => fs.GetUpdatedReservationsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Throws<Exception>();

        return operaService.Object;
    }
}

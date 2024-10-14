namespace SyncHms.Events.UnitTests.Tests.Fias.Base;

public abstract class FiasPostingHandlerTestsBase : PostTransactionsResponseHandlerTestsBase
{
    private protected static void CheckType(MockEventContext context)
    {
        Assert.Equal(2, context.SendMessages.Count);
        Assert.Empty(context.Breaks);
        Assert.IsType<ApplicationEnvironment>(context.SendMessages[0]);
        Assert.IsType<Check>(context.SendMessages[1]);
    }
    
    private protected static IFiasService GetFiasService(bool syncPostingMicros, FiasAnswerStatuses status)
    {
        var fiasService = new Mock<IFiasService>();

        fiasService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            SyncPostingMicros = syncPostingMicros,
            Rvc = 99,
            NextMicrosCheckNumber = default
        });

        var answer = new FiasPostingAnswer
        {
            AnswerStatus = status,
            ClearText = status.ToString()
        };
        
        fiasService
            .Setup(fs => fs.SendPostingAsync(It.IsAny<FiasPostingSimple>(), It.IsAny<int>()))
            .ReturnsAsync(answer);
        
        fiasService
            .Setup(fs => fs.SendPostingAsync(It.IsAny<FiasPostingRequest>(), It.IsAny<int>()))
            .ReturnsAsync(answer);

        return fiasService.Object;
    }
    
    protected static IFiasService GetFiasServiceWithThrow(bool syncPostingMicros)
    {
        var fiasService = new Mock<IFiasService>();

        fiasService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            SyncPostingMicros = syncPostingMicros,
            Rvc = 99
        });

        fiasService
            .Setup(fs => fs.SendPostingAsync(It.IsAny<FiasPostingSimple>(), It.IsAny<int>()))
            .Throws<Exception>();
        
        fiasService
            .Setup(fs => fs.SendPostingAsync(It.IsAny<FiasPostingRequest>(), It.IsAny<int>()))
            .Throws<Exception>();

        return fiasService.Object;
    }
}
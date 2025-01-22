namespace SyncHms.Events.UnitTests.Tests.Fias.Base;

public abstract class FiasPostingHandlerTestsBase
{
    private protected static IFiasService GetFiasService(FiasAnswerStatuses status)
    {
        var fiasService = new Mock<IFiasService>();

        fiasService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            TaxCodes = []
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
    
    protected static IFiasService GetFiasServiceWithThrow()
    {
        var fiasService = new Mock<IFiasService>();

        fiasService.SetupGet(fs => fs.Environment).Returns(new ApplicationEnvironment
        {
            TaxCodes = []
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
namespace SyncHms.Events.UnitTests.Cases.Fias;

internal class FiasPostingHandlerCases : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var fiasAnswerStatus in Enum.GetValues<FiasAnswerStatuses>())
            if (fiasAnswerStatus != FiasAnswerStatuses.Successfully)
                yield return [fiasAnswerStatus];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
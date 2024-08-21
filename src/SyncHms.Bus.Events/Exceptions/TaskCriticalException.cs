namespace SyncHms.Bus.Events;

/// <summary>
/// Исключение критической ошибки в процессе обработки события.
/// При возбуждении исключения процесс обработки будет прерван
/// и событие не будет возвращено обратно в очередь.
/// </summary>
public class TaskCriticalException(string? message, Exception? innerException)
    : Exception(message, innerException)
{
}

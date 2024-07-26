namespace SyncHms.Bus.Events;

public class TaskCriticalException(string? message, Exception? innerException)
    : Exception(message, innerException)
{
}

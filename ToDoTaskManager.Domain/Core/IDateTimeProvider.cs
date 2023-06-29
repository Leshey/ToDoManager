namespace ToDoTaskManager.Domain.Core;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}

public static class DateTimeProvider 
{
    private static IDateTimeProvider? _dateTimeProvider;

    public static DateTime UtcNow => _dateTimeProvider?.UtcNow ?? DateTime.UtcNow;

    public static void SetProvider(IDateTimeProvider dateTimeProvider) 
    {
        _dateTimeProvider = dateTimeProvider;
    }
}
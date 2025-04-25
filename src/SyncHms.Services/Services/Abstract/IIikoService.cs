namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом <c>IIKO</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IIikoService : IService<IikoOptions, ApplicationEnvironment>
{
    int TimeShiftSeconds { get; }
    
    string? DiscountCategoryName { get; }
    
    string? IncreaseCategoryName { get; }

    IReadOnlyDictionary<string, string> PaymentTypes { get; }

    IReadOnlyDictionary<string, int> SalesOutlet { get; }

    IReadOnlyList<string> Categories { get; }

    Task<string> LoginAsync();

    Task LogoutAsync(string token);

    Task<List<PaymentTransaction>> GetPaymentTransactionsAsync(
        string token,
        DateOnly fromInclude,
        DateOnly toExclude,
        params OlapReportFilter[] filters);

    Task<HashSet<OrderReturn>> GetOrderReturnedAsync(string token, DateTime fromInclude, DateTime toExclude);
}

namespace SyncHms.Services;

/// <summary>
/// Интерфейс, определяющий методы сервиса интеграции с удаленным сервисом <c>IIKO</c><br/>
/// Расширяет методы интерфейса <see cref="IService{TOptions,TEnvironment}"/>
/// </summary>
public interface IIikoService : IService<IikoOptions, ApplicationEnvironment>
{
    IReadOnlyDictionary<string, string> PayTypes { get; }

    IikoConfiguration RouteConfiguration { get; }

    Task<string> LoginAsync();

    Task LogoutAsync(string token);

    Task<List<PaymentTransaction>> GetPaymentTransactionsAsync(
        string token,
        DateOnly fromInclude,
        DateOnly toExclude,
        params OlapReportFilter[] filters);

    Task<HashSet<long>> GetOrderReturnedAsync(string token, DateTime fromInclude, DateTime toExclude);
}

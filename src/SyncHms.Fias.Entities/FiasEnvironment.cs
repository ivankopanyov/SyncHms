namespace SyncHms.Fias.Entities;

public static class FiasEnvironment
{
    public const char Separator = '|';

    public const char Head = '\x02';

    public const char Tail = '\x03';

    public const int Max1 = 9;

    public const int Min1 = 0;

    public const int Max2 = 99;

    public const int Min2 = -9;

    public const int Max4 = 9999;

    public const int Min4 = -999;

    public const int Max5 = 99999;

    public const int Min5 = -9999;

    public const int Max6 = 999999;

    public const int Min6 = -99999;

    public const int Max8 = 99999999;

    public const int Min8 = -9999999;

    public const long Max10 = 9999999999;

    public const long Min10 = -999999999;

    public const string Max15 = "999999999999999";

    public const string Min15 = "-99999999999999";

    public const string Max19 = "9999999999999999999";

    public const string Min19 = "-999999999999999999";

    public const string Max20 = "99999999999999999999";

    public const string Min20 = "-9999999999999999999";

    public const string AnPattern = "^[\x20-\x7F]*$";

    public const string FiasDateFormat = "yyMMdd";

    public const string FiasTimeFormat = "HHmmss";

    public const string FiasTimeFormatWithoutSeconds = "HHmm";

    public const string FiasTimeFormatDeparture = "HH:mm";

    private static readonly HashSet<Type> _messageTypes;

    private static readonly Dictionary<string, Type> _fromPmsIndicatorType;

    private static readonly Dictionary<string, Type> _toPmsIndicatorType;

    internal static IReadOnlyCollection<Type> MessageTypes => _messageTypes;

    internal static IReadOnlyDictionary<string, Type> FromPmsIndicatorType => _fromPmsIndicatorType;

    internal static IReadOnlyDictionary<string, Type> ToPmsIndicatorType => _toPmsIndicatorType;

    static FiasEnvironment()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetCustomAttribute<FiasMessageAttribute>() != null);

        _messageTypes = [];
        _toPmsIndicatorType = [];
        _fromPmsIndicatorType = [];

        foreach (var type in types)
        {
            if (type.GetCustomAttribute<FiasMessageAttribute>() is not { } attribute) continue;
            _messageTypes.Add(type);

            if (attribute.Direction.HasFlag(FiasMessageDirections.FromPms))
                _fromPmsIndicatorType.Add(attribute.Indicator, type);

            if (attribute.Direction.HasFlag(FiasMessageDirections.ToPms))
                _toPmsIndicatorType.Add(attribute.Indicator, type);
        }
    }
}
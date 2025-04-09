namespace SyncHms.Services.Converters;

internal class IikoDateTimeFormatConverter : IsoDateTimeConverter
{
    public IikoDateTimeFormatConverter()
    {
        DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.ffffff";
    }
}

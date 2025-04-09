namespace SyncHms.Services.Attributes;

public class JsonTypeAttribute<T> : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string json)
            return false;

        try
        {
            return JsonConvert.DeserializeObject<T>(json) is not null;
        }
        catch
        {
            return false;
        }
    }
}
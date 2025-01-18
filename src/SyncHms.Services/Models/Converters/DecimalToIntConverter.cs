namespace SyncHms.Services.Converters;

internal class DecimalToIntConverter : Newtonsoft.Json.JsonConverter<int>
{
    public override void WriteJson(JsonWriter writer, int value,  Newtonsoft.Json.JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }

    public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue,  Newtonsoft.Json.JsonSerializer serializer)
    {
        return Convert.ToInt32( reader.Value);
    }
}
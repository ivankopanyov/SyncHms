﻿namespace SyncHms.Fias.Entities.Json.Converters;

internal class FiasBoolConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => true;

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) =>
        (reader.Value?.ToString()) switch
        {
            "Y" => true,
            "N" => false,
            "" => false,
            _ => null
        };

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) =>
        writer.WriteValue((value is bool condition) ? (condition ? "Y" : "N") : null);
}

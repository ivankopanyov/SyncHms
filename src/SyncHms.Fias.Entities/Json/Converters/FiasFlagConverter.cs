﻿namespace SyncHms.Fias.Entities.Json.Converters;

internal class FiasFlagConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => true;

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) =>
        reader.Value != null ? true : null;

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) =>
        writer.WriteValue(value is null ? null : string.Empty);
}

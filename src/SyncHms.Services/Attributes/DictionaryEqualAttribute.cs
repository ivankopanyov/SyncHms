namespace SyncHms.Services.Attributes;

internal class DictionaryEqualAttribute<TKey, TValue>(params TKey[] keys) : ValidationAttribute()
{
    public override bool IsValid(object? value) =>
        value is Dictionary<TKey, TValue> dictionary && keys.ToHashSet().SetEquals(dictionary.Keys);
}

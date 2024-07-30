namespace SyncHms.Services.Attributes;

internal class EnumerableRegularExpressionAttribute([StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
    : RegularExpressionAttribute(pattern)
{
    public override bool IsValid(object? value) => value switch
    {
        IDictionary<string, bool> dictionary => Validate(dictionary.Keys),
        IEnumerable<string> enumerable => Validate(enumerable),
        _ => true
    };

    private bool Validate(IEnumerable<string?> value)
    {
        return value.All(str => base.IsValid(str));
    }
}
namespace SyncHms.Fias.Entities.Json.Writers;

internal sealed class FiasJsonWriter : JsonWriter
{
    private readonly TextWriter _writer;

    private string _currentPropertyName;

    private bool _isDelimeter = true;

    public FiasJsonWriter(TextWriter textWriter, string indicator)
    {
        _writer = textWriter;

        WriteStart();
        if (string.IsNullOrEmpty(indicator))
            return;
        
        WriteIndicator(indicator);
        WriteValueDelimiter();
    }

    private void WriteStart() => _writer.Write(FiasEnvironment.Head);

    private void WriteIndicator(string indicator) => _writer.Write(indicator);

    public override void WritePropertyName(string name)
    {
        base.WritePropertyName(name);
        _currentPropertyName = name;
    }

    protected override void WriteEnd(JsonToken token)
    {
        base.WriteEnd(token);
        WriteValueDelimiter();
        _writer.Write(FiasEnvironment.Tail);
    }

    protected override void WriteValueDelimiter()
    {
        base.WriteValueDelimiter();
        if (_isDelimeter)
            _writer.Write(FiasEnvironment.Separator);
    }

    public override void WriteNull()
    {
        base.WriteNull();
        _isDelimeter = false;
    }

    public override void WriteValue(string? value)
    {
        base.WriteValue(value);
        if (value != null)
            WriteItem(value);
        else
            WriteNull();
    }

    public override void WriteValue(int value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(uint value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(long value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(ulong value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(float value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(double value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(bool value) => base.WriteValue(value);

    public override void WriteValue(short value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(ushort value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(char value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(byte value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(sbyte value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(decimal value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(DateTime value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString("yyMMdd"));
    }

    public override void WriteValue(DateTimeOffset value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString("yyMMdd"));
    }

    public override void WriteValue(Guid value)
    {
        base.WriteValue(value);
        WriteItem(value.ToString());
    }

    public override void WriteValue(TimeSpan value)
    {
        base.WriteValue(value);
        TimeOnly timeOnly = value.Ticks > TimeOnly.MaxValue.Ticks ? TimeOnly.MaxValue : TimeOnly.FromTimeSpan(value);
        WriteItem(timeOnly.ToString("HHmmss"));
    }

    public override void WriteValue(object? value)
    {
        base.WriteValue(value);
        var str = value?.ToString();
        if (str != null)
            WriteItem(str);
        else
            WriteNull();
    }

    public override void Flush() => _writer.Flush();

    private void WriteItem(string value)
    {
        _writer.Write($"{_currentPropertyName}{value}");
        _isDelimeter = true;
    }
}

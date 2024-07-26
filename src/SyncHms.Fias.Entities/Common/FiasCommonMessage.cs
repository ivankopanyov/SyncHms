namespace SyncHms.Fias.Entities;

public sealed partial class FiasCommonMessage
{
    [JsonIgnore]
    private static readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        Error = (se, ev) => ev.ErrorContext.Handled = true
    };

    [JsonIgnore]
    public string Indicator { get; set; }


    [JsonIgnore]
    public string Source { get; internal set; }


    [JsonIgnore]
    public DateTime? DateTime
    {
        get => Date is { } date ? date.ToDateTime(Time ?? default) : null;

        set
        {
            if (value is { } dateTime)
            {
                Date = DateOnly.FromDateTime(dateTime);
                Time = TimeOnly.FromDateTime(dateTime);
            }
            else
            {
                Date = null;
                Time = null;
            }

        }
    }

    [JsonIgnore]
    public bool? ChangeStatus
    {
        get => RequestType switch
        {
            0 => false,
            1 => true,
            _ => null,
        };

        set => RequestType = value switch
        {
            true => 1,
            false => 0,
            _ => null,
        };
    }

    [JsonIgnore]
    public FiasLinkDescriptionRequestTypes RequestTypes
    {
        set => RequestType = value?.Sum();
    }

    [JsonIgnore]
    public bool? UseKeyAnswerTrack3Data
    {
        get => RequestType switch
        {
            0 => false,
            4 => true,
            _ => null,
        };

        set => RequestType = value switch
        {
            true => 4,
            false => 0,
            _ => null,
        };
    }

    [JsonIgnore]
    public decimal?[] Discounts
    {
        get => new decimal?[]
        {
            Discount1,
            Discount2,
            Discount3,
            Discount4,
            Discount5,
            Discount6,
            Discount7,
            Discount8,
            Discount9
        };

        set
        {
            Discount1 = value is { Length: > 0 } ? value[0] : null;
            Discount2 = value is { Length: > 1 } ? value[1] : null;
            Discount3 = value is { Length: > 2 } ? value[2] : null;
            Discount4 = value is { Length: > 3 } ? value[3] : null;
            Discount5 = value is { Length: > 4 } ? value[4] : null;
            Discount6 = value is { Length: > 5 } ? value[5] : null;
            Discount7 = value is { Length: > 6 } ? value[6] : null;
            Discount8 = value is { Length: > 7 } ? value[7] : null;
            Discount9 = value is { Length: > 8 } ? value[8] : null;
        }
    }

    [JsonIgnore]
    public decimal?[] Subtotals
    {
        get => new decimal?[]
        {
            Subtotal1,
            Subtotal2,
            Subtotal3,
            Subtotal4,
            Subtotal5,
            Subtotal6,
            Subtotal7,
            Subtotal8,
            Subtotal9
        };

        set
        {
            Subtotal1 = value is { Length: > 0 } ? value[0] : null;
            Subtotal2 = value is { Length: > 1 } ? value[1] : null;
            Subtotal3 = value is { Length: > 2 } ? value[2] : null;
            Subtotal4 = value is { Length: > 3 } ? value[3] : null;
            Subtotal5 = value is { Length: > 4 } ? value[4] : null;
            Subtotal6 = value is { Length: > 5 } ? value[5] : null;
            Subtotal7 = value is { Length: > 6 } ? value[6] : null;
            Subtotal8 = value is { Length: > 7 } ? value[7] : null;
            Subtotal9 = value is { Length: > 8 } ? value[8] : null;
        }
    }

    [JsonIgnore]
    public decimal?[] Taxes
    {
        get => new decimal?[]
        {
            Tax1,
            Tax2,
            Tax3,
            Tax4,
            Tax5,
            Tax6,
            Tax7,
            Tax8,
            Tax9
        };

        set
        {
            Tax1 = value is { Length: > 0 } ? value[0] : null;
            Tax2 = value is { Length: > 1 } ? value[1] : null;
            Tax3 = value is { Length: > 2 } ? value[2] : null;
            Tax4 = value is { Length: > 3 } ? value[3] : null;
            Tax5 = value is { Length: > 4 } ? value[4] : null;
            Tax6 = value is { Length: > 5 } ? value[5] : null;
            Tax7 = value is { Length: > 6 } ? value[6] : null;
            Tax8 = value is { Length: > 7 } ? value[7] : null;
            Tax9 = value is { Length: > 8 } ? value[8] : null;
        }
    }

    [JsonIgnore]
    public string[] UserDefinableFields => new string[]
    {
        UserDefinableField0,
        UserDefinableField1,
        UserDefinableField2,
        UserDefinableField3,
        UserDefinableField4,
        UserDefinableField5,
        UserDefinableField6,
        UserDefinableField7,
        UserDefinableField8,
        UserDefinableField9
    };

    [JsonIgnore]
    public IList<string>? SuiteInfoCollection => SuiteInfo?.Split(';');

    [JsonIgnore]
    public TimeSpan? EftTimeout1 =>
        UserDefinableField0?.Split(';') is { Length: > 0 } timeouts
        && int.TryParse(timeouts[0], out var timeout) && timeout >= 0
            ? TimeSpan.FromSeconds(timeout) : null;

    [JsonIgnore]
    public TimeSpan? DlsTimeout => 
        UserDefinableField0?.Split(';') is { Length: > 1 } timeouts
        && int.TryParse(timeouts[1], out var timeout) && timeout >= 0
            ? TimeSpan.FromSeconds(timeout) : null;

    [JsonIgnore]
    public string PmsVersion => UserDefinableField1;

    [JsonIgnore]
    public string IfcVersion => UserDefinableField2;

    [JsonIgnore]
    public string IfcDriverVersion => UserDefinableField3;

    [JsonIgnore]
    public string[]? RoomPaymentMethodsArray => RoomPaymentMethods?.Split(',');

    [JsonIgnore]
    public decimal? FidelioStandardTrack2Format
    {
        get => Track2Data != null && decimal.TryParse(Track2Data, out var value)
            ? value : null;

        set => Track2Data = value?.ToString();
    }
    public FiasCommonMessage() { }

    public FiasCommonMessage(string source) => Source = source;

    public FiasCommonMessage(string source, string indicator) : this(source) => Indicator = indicator;

    public object? ToFiasMessageToPmsObject() => 
        FiasEnvironment.ToPmsIndicatorType.TryGetValue(Indicator, out var type)
        ? FiasMapper.Mapper.Map(this, type) : null;

    public object? ToFiasMessageFromPmsObject() =>
        FiasEnvironment.FromPmsIndicatorType.TryGetValue(Indicator, out var type)
        ? FiasMapper.Mapper.Map(this, type) : null;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) =>
        (ToFiasMessageToPmsObject() ?? ToFiasMessageFromPmsObject()) is IValidatableObject validatableObject
            ? validatableObject.Validate(validationContext) : [];

    public override string ToString()
    {
        var stringWriter = new StringWriter();
        var serializer = new JsonSerializer();

        try
        {
            FiasJsonWriter writer = new FiasJsonWriter(stringWriter, Indicator);
            serializer.Serialize(writer, this);

            return stringWriter.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static FiasCommonMessage FromString(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            return new FiasCommonMessage(source);

        var items = source
            .TrimStart(FiasEnvironment.Head)
            .TrimEnd(FiasEnvironment.Tail)
            .Split(new[] { FiasEnvironment.Separator }, StringSplitOptions.RemoveEmptyEntries);

        if (items.Length == 0)
            return new FiasCommonMessage(source);

        var json = items.Length > 1
            ? string.Join(",", items.Skip(1).Select(item => item.Length <= 2 ? $"\"{item}\":\"\"" : $"\"{item[..2]}\":\"{item[2..].Replace("\"", "\\\"")}\""))
            : string.Empty;

        try
        {
            var fiasCommonMessage = JsonConvert.DeserializeObject<FiasCommonMessage>('{' + json + '}', _jsonSerializerSettings)
                ?? new FiasCommonMessage();

            fiasCommonMessage.Source = source;
            fiasCommonMessage.Indicator = items[0];
            return fiasCommonMessage;
        }
        catch
        {
            return new FiasCommonMessage(source, items[0]);
        }
    }
}


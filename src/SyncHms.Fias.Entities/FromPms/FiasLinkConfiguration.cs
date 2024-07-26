﻿namespace SyncHms.Fias.Entities;

public partial class FiasLinkConfiguration : FiasMessageFromPms
{
    public TimeSpan? EftTimeout { get; set; }

    public TimeSpan? DlsTimeout { get; set; }

    public string? PmsVersion { get; set; }

    public string? IfcVersion { get; set; }

    public string? IfcDriverVersion { get; set; }

    public string? HotelId { get; set; }

    /// <summary>
    /// Room Payment methods as defined in the PMS.<br/>
    /// Способы оплаты номера как определено в PMS.
    /// </summary>
    public string[] RoomPaymentMethodsArray { get; set; }

    /// <summary>
    /// Cryptogram (Only for EFT functionality).<br/>
    /// Криптограмма (только для функций EFT).
    /// </summary>
    public string? Cryptogram { get; set; }
}

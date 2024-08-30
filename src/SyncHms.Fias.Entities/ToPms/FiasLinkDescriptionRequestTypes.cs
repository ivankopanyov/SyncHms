namespace SyncHms.Fias.Entities;

public class FiasLinkDescriptionRequestTypes
{
    private int _configuredRoomPaymentMethods, _pmsVersion, _ifcVersion, _ifcDriverVersion, _dlsTimeout;

    /// <summary>
    /// Request the configured Room payment methods in <see cref="FiasLinkConfiguration.RoomPaymentMethods"/>
    /// </summary>
    public bool ConfiguredRoomPaymentMethods
    {
        get => _configuredRoomPaymentMethods == 1;
        set => _configuredRoomPaymentMethods = value ? 1 : 0;
    }

    /// <summary>Request PMS version in <see cref="FiasLinkConfiguration.PmsVersion"/></summary>
    public bool PmsVersion
    {
        get => _pmsVersion == 4;
        set => _pmsVersion = value ? 4 : 0;
    }

    /// <summary>Request IFC version in <see cref="FiasLinkConfiguration.IfcVersion"/></summary>
    public bool IfcVersion
    {
        get => _ifcVersion == 16;
        set => _ifcVersion = value ? 16 : 0;
    }

    /// <summary>Request IFC Driver Version in <see cref="FiasLinkConfiguration.IfcDriverVersion"/></summary>
    public bool IfcDriverVersion
    {
        get => _ifcDriverVersion == 32;
        set => _ifcDriverVersion = value ? 32 : 0;
    }

    /// <summary>Request DLS-timeout in <see cref="FiasLinkConfiguration.DlsTimeout"/></summary>
    public bool DlsTimeout
    {
        get => _dlsTimeout == 128;
        set => _dlsTimeout = value ? 128 : 0;
    }

    internal int? Sum()
    {
        var sum = _configuredRoomPaymentMethods + _pmsVersion + _ifcVersion
                  + _ifcDriverVersion + _dlsTimeout;

        return sum > 0 ? sum : null;
    }
}


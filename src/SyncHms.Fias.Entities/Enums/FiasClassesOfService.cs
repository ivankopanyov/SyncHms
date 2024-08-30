﻿namespace SyncHms.Fias.Entities;

public enum FiasClassesOfService
{
    [EnumMember(Value = "0")]
    BarredOrHotelInternalOnly,

    [EnumMember(Value = "1")]
    Local,

    [EnumMember(Value = "2")]
    National,

    [EnumMember(Value = "3")]
    NoRestrictions
}

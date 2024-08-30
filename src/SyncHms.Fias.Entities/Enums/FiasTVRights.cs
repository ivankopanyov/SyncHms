namespace SyncHms.Fias.Entities;

public enum FiasTvRights
{
    [EnumMember(Value = "TU")]
    UnlimitedPayChannels,

    [EnumMember(Value = "TM")]
    NoPayMovies,

    [EnumMember(Value = "TX")]
    NoAdultMovies,

    [EnumMember(Value = "TN")]
    NoTvRights
}

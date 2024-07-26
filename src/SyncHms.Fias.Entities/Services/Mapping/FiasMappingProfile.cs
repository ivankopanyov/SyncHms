namespace SyncHms.Fias.Entities.Services.Mapping;

internal class FiasMappingProfile : Profile
{
    public FiasMappingProfile()
    {
        foreach (var type in FiasEnvironment.MessageTypes)
            CreateMap(typeof(FiasCommonMessage), type).ReverseMap();
    }
}


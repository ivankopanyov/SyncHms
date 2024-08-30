﻿namespace SyncHms.Fias.Entities.Services.Mapping;

internal sealed class FiasMapper : IFiasMapper
{
    private static FiasMapper? _fiasMapper;

    public static IFiasMapper Mapper => _fiasMapper ??= new FiasMapper();

    private readonly IMapper _mapper;

    private FiasMapper() => _mapper ??= new MapperConfiguration(OnConfiguring).CreateMapper();

    public FiasCommonMessage Map(object source, Type sourceType) =>
        (FiasCommonMessage)_mapper.Map(source, sourceType, typeof(FiasCommonMessage));

    public object Map(FiasCommonMessage source, Type destType) =>
        _mapper.Map(source, typeof(FiasCommonMessage), destType);

    private static void OnConfiguring(IMapperConfigurationExpression configuration) =>
        configuration.AddProfile<FiasMappingProfile>();
}


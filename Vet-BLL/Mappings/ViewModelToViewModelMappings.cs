using AutoMapper;

namespace Vet_BLL.Mappings
{
    public class ViewModelToViewModelMappings
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            //config.CreateMap<ScoresTypes, vmStudent.PointCreateStudent>()
            //    .ForMember(d => d.ScoresTypesId, o => o.MapFrom(s => s.ScoresTypesId))
            //    .ForMember(d => d.NameScoreType, o => o.MapFrom(s => s.ScoresTypeName));
        }
    }
}
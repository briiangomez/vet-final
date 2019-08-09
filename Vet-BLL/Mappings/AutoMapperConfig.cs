using AutoMapper;

namespace Vet_BLL.Mappings
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(config =>
            {
                ModelToViewModelMappings.Configure(config);
                ViewModelToModelMappings.Configure(config);
                ViewModelToViewModelMappings.Configure(config);
            });
        }
    }
}
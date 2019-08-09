using AutoMapper;
using Vet_Core.ViewModels;
using Vet_Data.Models;

namespace Vet_BLL.Mappings
{
    public class ViewModelToModelMappings
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<Factura, FacturaVM>();
            config.CreateMap<ItemFactura, ItemFacturaVM>();
            config.CreateMap<Mascota, MascotaVM>();
            config.CreateMap<Turno, TurnoVM>();
            config.CreateMap<Cliente, ClienteVM>();
        }
    }
}
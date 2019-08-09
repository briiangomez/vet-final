using System;
using AutoMapper;
using Vet_Core.ViewModels;
using Vet_Data.Models;
namespace Vet_BLL.Mappings
{
    public class ModelToViewModelMappings
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<Factura, FacturaVM>();
            config.CreateMap<ItemFactura, ItemFacturaVM>();
            config.CreateMap<Mascota, MascotaVM>();
            config.CreateMap<Turno, TurnoVM>();
            config.CreateMap<Cliente, ClienteVM>();
            //config.CreateMap<Atencion, AtencionVM>()
            //    .ForMember(d => d.InicioStr, o => o.MapFrom(s => s.Inicio.ToString("dd/MM/yyyy HH:mm")))
            //    .ForMember(d => d.FinStr, o => o.MapFrom(s => s.Fin.Value != null ? s.Fin.Value.ToString("dd/MM/yyyy HH:mm") : DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm")));
        }

        public static string NormalizeDateTimeNull(DateTime dateTimeFromServer)
        {
            if (dateTimeFromServer.Equals(null))
            {
                return string.Empty;
            }

            return dateTimeFromServer.ToString("dd/MM/yyyy HH:mm");
        }

        public static decimal Normalize(decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
    }
}
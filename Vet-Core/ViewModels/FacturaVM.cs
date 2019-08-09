using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Models;

namespace Vet_Core.ViewModels
{
    public class FacturaVM
    {
        public int ID { get; set; }
        public int Numero { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public int TurnoId { get; set; }
        public decimal Importe { get; set; }
        public int LetraFactura { get; set; }
        public int FormaPago { get; set; }
        public List<ItemFacturaVM> ItemFactura { get; set; }
        public TurnoVM Turno { get; set; }
        public ClienteVM Cliente { get; set; }
    }
}

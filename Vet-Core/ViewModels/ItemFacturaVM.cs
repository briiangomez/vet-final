using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_Core.ViewModels
{
    public class ItemFacturaVM
    {
        public int ID { get; set; }
        public int ItemId { get; set; }
        public int FacturaId { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Precio { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Context;
using Vet_Data.Models;

namespace Vet_Core.Repositories
{
    public class FacturaRepository : GenericRepository<Factura>
    {

        public List<ItemFactura> ObtenerItemFactura(int idFactura)
        {
            return this.Find(idFactura).ItemFactura.ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Models;
using Vet_Data.Context;

namespace Vet_Core.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>
    {
        public List<Mascota> ObtenerMascotasCliente(int idCliente)
        {
            return this.Find(idCliente).Mascota.ToList();
        }
    }
}

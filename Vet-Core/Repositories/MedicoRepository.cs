using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Context;
using Vet_Data.Models;

namespace Vet_Core.Repositories
{
    public class MedicoRepository : GenericRepository<Medico>
    {
        public List<Medico> ObtenerMedicoByEspecialidad(int idEspecialidad)
        {
            return List()
                .Where(e => e.Especialidades.Any(de => de.EspecialidadID == idEspecialidad))
                .ToList();
        }
    }
}
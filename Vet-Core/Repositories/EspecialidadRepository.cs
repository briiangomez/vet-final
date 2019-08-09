using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Context;
using Vet_Data.Models;

namespace Vet_Core.Repositories
{
    public class EspecialidadRepository : GenericRepository<Especialidad>
    {
        public List<Especialidad> ObtenerEspecialidadesByDoctor(int idMedico)
        {
            return List()
                .Where(e => e.Medicos.Any(de => de.MedicoID == idMedico))
                .ToList();
        }
        public TimeSpan ObtenerDuracionEspecialidad(int idMedico, int idEsp)
        {
            var especialidades = ObtenerEspecialidadesByDoctor(idMedico);
            var espMed = especialidades.SelectMany(x => x.Medicos);
            int dur = espMed.Where(x => x.EspecialidadID == idEsp && x.MedicoID == idMedico).Single().Especialidades.Duracion;
            return new TimeSpan(0, dur, 0);
        }
        public TimeSpan ObtenerMinimaDuracion(int idMedico)
        {
            var especialidades = ObtenerEspecialidadesByDoctor(idMedico);
            var espMed = especialidades.SelectMany(s => s.Medicos);
            int dur = espMed.Where(x => x.MedicoID == idMedico).Select(z => z.Especialidades.Duracion).Min();
            return new TimeSpan(0, dur, 0);
        }
    }
}


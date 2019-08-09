using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_Core.ViewModels
{
    public class TurnoVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Seleccione Medico")]
        public int MedicoId { get; set; }
        [Required(ErrorMessage = "Seleccione Especialidad")]
        public int EspecialidadId { get; set; }
        [Required(ErrorMessage = "Seleccione Mascota")]
        public int MascotaId { get; set; }
        [Required(ErrorMessage = "Seleccione Sala")]
        public int SalaId { get; set; }
        [Required(ErrorMessage = "Seleccione Item")]
        public int ItemId { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Seleccione Fecha")]
        public string Fecha { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int EstadoId { get; set; }
    }
}

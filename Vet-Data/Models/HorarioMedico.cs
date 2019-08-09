using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class HorarioMedico : IEntity
    {
        public int ID { get; set; }
        [Required]
        public DayOfWeek Dia { get; set; }
        [Required]
        [DisplayName("Desde")]
        public DateTime HoraDesde { get; set; }
        [Required]
        [DisplayName("Hasta")]
        public DateTime HoraHasta { get; set; }
        [ForeignKey("Medico"), Required]
        public int MedicoId { get; set; }

        public virtual Medico Medico { get; set; }

    }
}

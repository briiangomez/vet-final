using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_Data.Models
{
    public class MedicoEspecialidad
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Medico")]
        public int MedicoID { get; set; }

        [DisplayName("Especialidad")]
        public int EspecialidadID { get; set; }
        public virtual Medico Medico { get; set; }

        [DisplayName("Especialidad")]
        public virtual Especialidad Especialidades { get; set; }
    }
}

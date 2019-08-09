using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vet_Data.Models;

namespace Vet_Core.ViewModels
{
    public class MascotaVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Ingrese Nombre")]
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "Ingrese Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Seleccione Cliente")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Seleccione Raza")]
        public int RazaId { get; set; }
        public string Foto { get; set; }

        public Cliente Cliente { get; set; }
        public Raza Raza { get; set; }
        public List<TurnoVM> Turno { get; set; }
    }
}

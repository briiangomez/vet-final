using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class Medico : IEntity
    {
        public Medico()
        {
            this.Turno = new HashSet<Turno>();
            this.Especialidades = new HashSet<MedicoEspecialidad>();
            this.Horarios = new HashSet<HorarioMedico>();
        }
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Ingrese Matricula")]
        public int Matricula { get; set; }
        [Required(ErrorMessage = "Ingrese Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese Apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Ingrese Numero de Documento")]
        public int NroDocumento { get; set; }
        [Required(ErrorMessage = "Ingrese Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + Apellido;
            }
        }
        [NotMapped]
        public DiaSemana dias { get; set; }
        public virtual ICollection<Turno> Turno { get; set; }
        public virtual ICollection<MedicoEspecialidad> Especialidades { get; set; }
        public virtual ICollection<HorarioMedico> Horarios { get; set; }
    }
}

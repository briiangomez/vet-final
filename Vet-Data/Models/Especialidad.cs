using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{

    public partial class Especialidad : IEntity
    {
        public Especialidad()
        {
            this.Medicos = new HashSet<MedicoEspecialidad>();
        }
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }

        public virtual ICollection<MedicoEspecialidad> Medicos { get; set; }
    }
}

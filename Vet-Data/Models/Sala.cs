using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class Sala : IEntity
    {
        public Sala()
        {
            this.Turno = new HashSet<Turno>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage = "Ingrese Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese Locacion")]
        public string Locacion { get; set; }
    
        public virtual ICollection<Turno> Turno { get; set; }
    }
}

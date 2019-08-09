using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class Raza : IEntity
    {
        public Raza()
        {
            this.Mascota = new HashSet<Mascota>();
        }
    
        public int ID { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Mascota> Mascota { get; set; }
    }
}

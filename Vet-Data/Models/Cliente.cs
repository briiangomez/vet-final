using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{

    public partial class Cliente : IEntity
    {
        public Cliente()
        {
            this.Factura = new HashSet<Factura>();
            this.Mascota = new HashSet<Mascota>();
        }
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Localidad { get; set; }
        public int NroDocumento { get; set; }

        [NotMapped]
        public string NombreCompleto
        {
            get
            {
                return this.Nombre + " " + this.Apellido; 
            }
            set
            {

            }
        }

        public virtual ICollection<Factura> Factura { get; set; }
        public virtual ICollection<Mascota> Mascota { get; set; }
    }
}

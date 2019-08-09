using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class Mascota : IEntity
    {
        public Mascota()
        {
            this.Turno = new HashSet<Turno>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [DisplayName("Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        //[ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        [ForeignKey("Raza")]
        public int RazaId { get; set; }
        public string Foto { get; set; }

        [NotMapped]
        public HttpPostedFileBase imagen { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Raza Raza { get; set; }
        public virtual ICollection<Turno> Turno { get; set; }
    }
}

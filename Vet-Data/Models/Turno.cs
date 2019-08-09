using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class Turno : IEntity
    {
        public Turno()
        {
            this.Factura = new HashSet<Factura>();
        }
        [Key]
        public int ID { get; set; }
        [ForeignKey("Medico")]
        public int MedicoId { get; set; }
        [ForeignKey("Especialidad")]
        public int EspecialidadId { get; set; }
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }
        [ForeignKey("Sala")]
        public int SalaId { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? FechaFin { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
        public virtual Sala Sala { get; set; }
        public virtual Item Item { get; set; }
        public virtual Mascota Mascota { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual EstadoTurno Estado { get; set; }
        public virtual Especialidad Especialidad { get; set; }
    }
}

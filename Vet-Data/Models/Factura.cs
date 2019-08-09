using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{

    public partial class Factura : IEntity
    {
        public Factura()
        {
            this.ItemFactura = new HashSet<ItemFactura>();
        }
        public int ID { get; set; }
        public int Numero { get; set; }
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey("Turno")]
        public int TurnoId { get; set; }
        public decimal Importe { get; set; }
        public virtual Cliente Cliente { get; set; }
        [DisplayName("Letra")]
        public virtual LetraFactura LetraFactura { get; set; }
        [DisplayName("Forma de pago")]
        public virtual FormaPago FormaPago { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual ICollection<ItemFactura> ItemFactura { get; set; }
    }
}

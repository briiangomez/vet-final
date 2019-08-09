using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class ItemFactura : IEntity
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Factura")]
        //[Column(Order = 1)]
        public int FacturaId { get; set; }
        [ForeignKey("Item")]
        //[Column(Order = 2)]
        public int ItemId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }

        public decimal Total { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Item Item { get; set; }
    }
}

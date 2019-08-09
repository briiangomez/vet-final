using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vet_Data.Interfaces;

namespace Vet_Data.Models
{
    public partial class Item : IEntity
    {
        public Item()
        {
            this.ItemFactura = new HashSet<ItemFactura>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage = "Ingrese Descripcion")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Ingrese Valor")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Seleccione Tipo")]
        public TipoItem Tipo { get; set; }
    
        public virtual ICollection<ItemFactura> ItemFactura { get; set; }
    }
}

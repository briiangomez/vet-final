using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vet_Core.ViewModels
{
    public class ClienteVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese Apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Ingrese Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Ingrese Direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Ingrese Localidad")]
        public string Localidad { get; set; }
        [Required(ErrorMessage = "Ingrese NroDocumento")]
        public int NroDocumento { get; set; }
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

        public List<FacturaVM> Factura { get; set; }
        public List<MascotaVM> Mascota { get; set; }
    }
}

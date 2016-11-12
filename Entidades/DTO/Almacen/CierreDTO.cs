using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Almacen
{
    public class CierreDTO
    {
        [Required]
        public int AlmacenId { get; set; }

        [Required]
        public string UserName { get; set; }

        public string AdministradorId { get; set; }

        public DateTime Fecha { get; set; }

        [Required]
        public double Efectivo { get; set; }

        [Required]
        public double Bancos { get; set; }

        [Required]
        public double Facturas { get; set; }

        [Required]
        public double Invercion { get; set; }

        [Required]
        public double Costos { get; set; }

    }
}

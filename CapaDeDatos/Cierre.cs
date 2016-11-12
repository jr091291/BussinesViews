using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class Cierre
    {
        public int CierreId { get; set; }

        [Required]
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

        [Required]
        public int AlmacenId { get; set; }

        public virtual Almacen Almacen { get; set; }
       
    }
}

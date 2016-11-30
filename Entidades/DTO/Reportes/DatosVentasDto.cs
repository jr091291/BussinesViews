using Entidades.DTO.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class DatosVentasDto
    {
        public string Fecha { get; set; }
        public double Efectivo { get; set; }
        public double Bancos { get; set; }
        public double Venta { get; set; }
        
    }
}

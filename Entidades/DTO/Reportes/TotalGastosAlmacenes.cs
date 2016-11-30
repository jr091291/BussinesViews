using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalGastosAlmacenes
    {
        public List<TotalGastosAlmacen> CostosPorAlmacen { get; set; }
        public double TotalGastos { get; set; }
    }
}

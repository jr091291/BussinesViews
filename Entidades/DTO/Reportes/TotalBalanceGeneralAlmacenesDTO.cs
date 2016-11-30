using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalBalanceGeneralAlmacenesDTO
    {
        public List<TotalBalanceGeneralAlmacen> BalancePorAlmacen { get; set; }
        public double TotalVentas { get; set; }
        public double TotalCostos { get; set; }
        public double TotalGastos { get; set; }
        public double TotalUtilidad { get; set; }
    }
}

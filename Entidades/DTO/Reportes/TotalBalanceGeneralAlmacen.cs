using Entidades.DTO.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalBalanceGeneralAlmacen
    {
        public AlmacenDTO Almacen { get; set; }
        public List<BalanceGeneralDTO> balances { get; set; }
        public double TotalVentas { get; set; }
        public double TotalCostos { get; set; }
        public double TotalGastos{ get; set; }
        public double TotalUtilidad {get; set;}
    }
}

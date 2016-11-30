using Entidades.DTO.reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalCostosAlmacenesDTO
    {
        public List<TotalCostosAlmacenDTO> costosPorAlmacen { get; set; }
        public double TotalInverciones { get; set; }
        public double TotalFacturas { get; set; }
        public double  TotalCostos { get; set; }

    }
}

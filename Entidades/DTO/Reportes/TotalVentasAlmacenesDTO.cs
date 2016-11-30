using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalVentasAlmacenesDTO
    {
        public List<TotalVentasAlmacenDTO> ventasPorAlmacen { get; set; }
        public double TotalEfectivo { get; set; }
        public double TotalBancos { get; set; }
        public double TotalVentas { get; set; }
    }
}

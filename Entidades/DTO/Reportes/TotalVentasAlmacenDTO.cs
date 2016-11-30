using Entidades.DTO.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalVentasAlmacenDTO
    {
        public AlmacenDTO Almacen { get; set; }
        public List<DatosVentasDto> ventas { get; set; }
        public double TotalEfectivo { get; set; }
        public double TotalBancos { get; set; }
        public double TotalVentas { get; set; }
    }
}

using Entidades.DTO.Almacen;
using Entidades.DTO.reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.reportes
{
    public class TotalCostosAlmacenDTO
    {
        public AlmacenDTO Almacen { get; set; }
        public List<DatosCostosDTO> costos { get; set; }
        public double TotalInversion { get; set; }
        public double TotalFacturas { get; set; }
        public double TotalCostos { get; set; }
    }
}

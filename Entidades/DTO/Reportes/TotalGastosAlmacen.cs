using Entidades.DTO.Almacen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class TotalGastosAlmacen
    {
       
            public AlmacenDTO Almacen { get; set; }
            public List<DatosGastosDTO> Gastos { get; set; }
            public double TotalGastos { get; set; }
        
    }
}

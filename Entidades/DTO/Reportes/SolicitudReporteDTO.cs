﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Reportes
{
    public class SolicitudReporteDTO
    {
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public List<Almacen.AlmacenDTO> ListadoAlmacenes { get; set; }
        //Listo así, pero ahora toca corregir algo en los reportes 
    }
}

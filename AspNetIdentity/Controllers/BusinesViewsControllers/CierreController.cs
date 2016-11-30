using AspNetIdentity.WebApi.Controllers;
using CapaDeLogica;
using Entidades.DTO.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AspNetIdentity.Controllers.BusinesViewsControllers
{
    [RoutePrefix("apiv1")]
    public class CierreController : BaseApiController
    {
        [HttpPost]
        [Route("cierre/ventas")]
        public IHttpActionResult ReportesCierre(SolicitudReporteDTO solicitud)
        {
            return Ok(new CierreBLL().getVentas(solicitud));
        }

        [HttpPost]
        [Route("cierre/costos")]
        public IHttpActionResult ReportesCostos(SolicitudReporteDTO solicitud)
        {
            return Ok(new CierreBLL().getCostos(solicitud));
        }

        [HttpPost]
        [Route("cierre/gastos")]
        public IHttpActionResult ReportesGastos(SolicitudReporteDTO solicitud)
        {
            return Ok(new CierreBLL().getGastos(solicitud));
        }
    }
}
using AspNetIdentity.WebApi.Controllers;
using CapaDeLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AspNetIdentity.Controllers.BusinesViewsControllers
{
    [RoutePrefix("apiv1")]
    public class UserController : BaseApiController
    {
        [HttpGet]
        [Route("almacen/{id:guid}/user")]
        public IHttpActionResult getAlmacenesList(string id)
        {
            var adminList =  new AdministradorBLL().getAlmacenAdmin(id);
            var invercionistaList =  new InvercionistaBLL().getAlmacenInv(id);
            return Ok(new Entidades.DTO.Almacen.AlmacenListDTO()
            {
                inversiones = invercionistaList,
                admin = adminList
            });
        }
    }
}
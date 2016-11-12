using AspNetIdentity.WebApi.Controllers;
using CapaDeLogica;
using Entidades.DTO.Almacen;
using Entidades.DTO.Response;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AspNetIdentity.Controllers
{
    [RoutePrefix("apiv1")]
    public class AlmacenController : BaseApiController 
    {
        [HttpPost]
        [Route("create/almacen")]
        public IHttpActionResult CreateAlmacen(AlmacenDTO model)
        {
            return GetActionResult(new Callback(CreateAlmacen) , model);
        }

        [HttpPost]
        [Route("create/administrador")]
        public async Task<IHttpActionResult> CreateAdmin(AdministradorDTO admin)
        {
            admin.UserId = "";
            Respuesta response = new Respuesta();
            UsuarioAlmacenDTO adminDTO = await this.getVerificarUserAlmacen(admin.UserName, admin.AlmacenId);

            if (adminDTO.response != null)
            {
                return Ok(adminDTO.response);
            }

            if (adminDTO.AlmacenId != 0 && adminDTO.UserId !=null) {
                admin.UserId = adminDTO.UserId;
                admin.AlmacenId = adminDTO.AlmacenId;
                return GetActionResult(new Callback(CreateAdmin), admin);
            }

            response.Errors.Add(new ResponseErrorDTO { Code = "404", Mensagge = "Verifique La Informacion Suministrada" });
            response.Mensagge = "No se encontro Informacion";
            return Ok(response);
        }

        [HttpPost]
        [Route("create/invercionista")]
        public async Task<IHttpActionResult> CreateInvercionista(InversionistaDTO invercionista)
        {
            Respuesta response = new Respuesta();
            UsuarioAlmacenDTO adminDTO = await this.getVerificarUserAlmacen(invercionista.UserName, invercionista.AlmacenId);

            if (adminDTO.response != null)
            {
                return Ok(adminDTO.response);
            }

            if (adminDTO.AlmacenId != 0 && adminDTO.UserId != null)
            {
                invercionista.UserId = adminDTO.UserId;
                invercionista.AlmacenId = adminDTO.AlmacenId;
                return GetActionResult(new Callback(CreateInvercionistaAlmacen), invercionista);
            }
            response.Errors.Add(new ResponseErrorDTO { Code="404", Mensagge="Verifique La Informacion Suministrada"});
            response.Mensagge = "No se encontro Informacion";
            return Ok(response);
        }

        [HttpPost]
        [Route("create/cierre")]
        public async Task<IHttpActionResult> createCierre(CierreDTO cierre)
        {
            Respuesta response = new Respuesta();
            cierre.Fecha =  DateTime.Now.Date;

            var user = await this.AppUserManager.FindByNameAsync(cierre.UserName);
            if (user == null) {
                response.Mensagge = "El Usuario No Existe";
                response.Errors.Add(new ResponseErrorDTO("", "El Usuario No se Encuentra Registrado, Verifique el nombre de usuario"));
                return Ok(response);
            }

            RespuestaDTO<AdministradorDTO> administradorQuery = await new AdministradorBLL().FindByIdAdmin(user.Id);
            if (administradorQuery.Errors.Count > 0)
            {
                return Ok(administradorQuery);
            }
            if (administradorQuery.Data.AlmacenId != cierre.AlmacenId) {
                response.Mensagge = "El Usuario Sin Permisos";
                response.Errors.Add(new ResponseErrorDTO("","El Usuario No Tiene Permisos Sobre Este Almacen"));
            }
            cierre.AdministradorId = user.Id;
            return GetActionResult(new Callback(CreateCierre), cierre);
        }

        private RespuestaDTO<AlmacenDTO> CreateAlmacen(Object model) {
            return new AlmacenBLL().InsertarAlmacen((AlmacenDTO) model);
        }

        private RespuestaDTO<AdministradorDTO> CreateAdmin(Object model)
        {
            return new AdministradorBLL().InsertarAdministrador((AdministradorDTO) model);
        }

        private RespuestaDTO<InversionistaDTO> CreateInvercionistaAlmacen(Object model)
        {
            return new InvercionistaBLL().InsertarInvercionista((InversionistaDTO)model);
        }

        private RespuestaDTO<CierreDTO> CreateCierre(Object model)
        {
            return new CierreBLL().InsertarCierre((CierreDTO) model);
        }


        private async Task<UsuarioAlmacenDTO> getVerificarUserAlmacen(string userName, int idAlmacen)
        {
            Respuesta response = new Respuesta();
            UsuarioAlmacenDTO userDTO = new UsuarioAlmacenDTO();

            var user = await this.AppUserManager.FindByNameAsync(userName);

            if (user == null)
            {
                response.Errors.Add(new ResponseErrorDTO() { Code = "", Mensagge = "El Usuario No Existe" });
                response.Mensagge = "Verifique El Nombre Del Usuario";
                userDTO.response = response;
                return userDTO;
            }

            RespuestaDTO<AlmacenDTO> almacen = await new AlmacenBLL().FindAlmacenById(idAlmacen);

            if (almacen == null || almacen.Errors.Count > 0)
            {
                if (almacen.Errors.Count > 0)
                {
                    response.Errors = almacen.Errors;
                    response.Mensagge = "Se Ha Presentado Un Error";
                }
                else
                {
                    response.Errors.Add(new ResponseErrorDTO() { Code = "", Mensagge = "El Almacen No Existe" });
                    response.Mensagge = "Verifique El Id Del Almacen.";
                }
                userDTO.response = response;
                return userDTO;
            }
            userDTO.AlmacenId = almacen.Data.AlmacenId;
            userDTO.UserId = user.Id;
            return userDTO;
        }

        private class UsuarioAlmacenDTO
        {
            public Respuesta response { get; set; }
            public int AlmacenId { get; set; }
            public string UserId { get; set; }
        };
    }
}
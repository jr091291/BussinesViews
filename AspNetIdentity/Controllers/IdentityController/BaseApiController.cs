using AspNetIdentity.Infrastructure;
using AspNetIdentity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.Http;
using System.Net;
using Microsoft.AspNet.Identity.Owin;
using Entidades.DTO.Response;
using System.Net.Http;

namespace AspNetIdentity.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        //Code removed from brevity
        private ApplicationRoleManager _AppRoleManager = null;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public delegate Respuesta Callback(object data);

        protected bool ValidateModel()
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            return true;
        }

        protected IHttpActionResult GetResults(Respuesta respuesta)
        {
            if (respuesta == null)
            {
                return NotFound();
            }
            return Ok(respuesta);
        }

        protected IHttpActionResult SetHttpCodeResult(HttpStatusCode status, Respuesta respuesta)
        {
            return Content(status, respuesta);
        }

        protected Respuesta ExecuteRequest(Callback callback, Object data)
        {
            return callback(data);
        }

        protected IHttpActionResult GetActionResult(Callback callback, Object data)
        {
            if (!ValidateModel())
            {
                return BadRequest(ModelState);
            }
            else
            {
                return this.GetResults(callback(data));
            }

        }
        public BaseApiController()
        {
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
                }
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return NotFound();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
     }
 }
}
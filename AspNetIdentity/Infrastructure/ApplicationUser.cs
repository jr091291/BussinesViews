using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string PrimerNombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }

        [MaxLength(50)]
        public string SegundoNombre { get; set; }

       
        [MaxLength(50)]
        public string SegundoApellido { get; set; }

        public DateTime LicenseExpiration { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }

}
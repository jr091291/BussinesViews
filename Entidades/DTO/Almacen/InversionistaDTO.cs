using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Almacen
{
    public class InversionistaDTO
    {
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int AlmacenId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Almacen
{
    public class AlmacenDTO
    {
        public int AlmacenId { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "El Nombre del Almacen, debe tener Maximo 60 Caracteres")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(60)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "El Telefono debe tener Maximo 15 Caracteres")]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(80 , ErrorMessage="El Correo No Debe Exeder De 80 Caracteres")]
        public string Correo { get; set; }

    }
}
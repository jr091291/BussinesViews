using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class Almacen
    {
        public int AlmacenId { get; set; }

        [Required]
        [StringLength(60)]
        [Index(IsUnique = true)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(60)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(13)]
        public string Telefono { get; set; }

        [StringLength(80)]
        public string Correo { get; set; }
        
    }
}

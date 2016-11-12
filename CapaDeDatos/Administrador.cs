using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class Administrador
    {
        public string AdministradorId { get; set; }
        public int AlmacenId { get; set; }
        public virtual Almacen Almacen { get; set; }
    }
}

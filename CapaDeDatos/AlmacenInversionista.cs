using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeDatos
{
    public class AlmacenInversionista
    {
        [Key, Column(Order = 0)]
        public int AlmacenId { get; set; }

        [Key, Column(Order = 1)]
        public string InversionistaId { get; set; }
        
        public virtual Invercionista Invercionista { get; set; }

      
        public virtual Almacen Almacen { get; set; }
    }
}

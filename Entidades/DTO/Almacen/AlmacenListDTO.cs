using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Almacen
{
    public class AlmacenListDTO
    {
        public IList<AlmacenDTO> inversiones { get; set; }
        public IList<AlmacenDTO> admin { get; set; }
    }
}

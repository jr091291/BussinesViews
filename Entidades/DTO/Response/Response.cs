using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO.Response
{
    public class Respuesta
    {
        public Respuesta(){
            Mensagge = "";
            Errors = new List<ResponseErrorDTO>();
            Rows = 0;
        }

        public Respuesta(string mensaje, List<ResponseErrorDTO> errors, int filasAfectadas) {
            Mensagge = mensaje;
            Errors = errors;
            Rows = filasAfectadas;
        }
        
        public string Mensagge { get; set; }

        public List<ResponseErrorDTO> Errors { get; set; }
 
        public int Rows { get; set; }
    }
}

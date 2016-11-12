namespace Entidades.DTO.Response
{
    public class ResponseErrorDTO
    {
        public ResponseErrorDTO() {
            Code = "";
            Mensagge = "";
        }

        public ResponseErrorDTO(string code, string mensagge) {
            Code = code;
            Mensagge = mensagge;
        }

        public string Code { get; set; }
        public string Mensagge { get; set; }
    }
}
using CapaDeDatos;
using Entidades.DTO.Almacen;
using Entidades.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeLogica
{
    public class InvercionistaBLL
    {
        Contexto db = new Contexto();

        public RespuestaDTO<InversionistaDTO>   InsertarInvercionista(InversionistaDTO invercionitaModel)
        {
            using (db = new Contexto())
            {
                RespuestaDTO<InversionistaDTO> response = new RespuestaDTO<InversionistaDTO>();
                try
                {
                    if (invercionitaModel.UserId == null)
                    {
                        response.Mensagge = "Ingrese El Id Del Usuario";
                        response.Errors.Add(new ResponseErrorDTO { Code = "404", Mensagge = "El Id SE Envio Vacio." });
                        return response;
                    }

                    // preparar el invercioinista para guardar
                    Invercionista invercionista = new Invercionista();
                    invercionista.InvercionistaId = invercionitaModel.UserId;

                    var inv = db.Invercionistas.Find(invercionitaModel.UserId);
                    if (inv == null)
                    {
                        db.Invercionistas.Add(invercionista);
                        response.Rows = db.SaveChanges();
                        if (response.Rows == 0)
                        {
                            response.Mensagge = "No Se Pudo Guardar El Invercionista. ";
                            response.Errors.Add(new ResponseErrorDTO("", "Error Al almacenar el invercionista."));
                            return response;
                        }
                    }
                    
                    AlmacenInversionista inver = new AlmacenInversionista();
                    inver.AlmacenId = invercionitaModel.AlmacenId;
                    inver.InversionistaId = invercionitaModel.UserId;
                    db.AlmacenInversionistas.Add(inver);
                    response.Rows = db.SaveChanges();
                    response.Mensagge = "Se Ha Guaradado El Invercionista satisfactoriamente";
                    response.Data = invercionitaModel;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    response.Mensagge = "Se Ha Presentado Un Error Al Guardar El Invercionista";
                    response.Errors.Add(new ResponseErrorDTO(ex.GetHashCode().ToString(), ex.Message));
                    return response;
                }
                catch (Exception ex)
                {
                    response.Mensagge = "Se Ha Presentado Un Error";
                    response.Errors.Add(new ResponseErrorDTO(ex.GetHashCode().ToString(), ex.Message));
                    response.Errors.Add(new ResponseErrorDTO("", "El Invercionista Ya Ha Sido Agregado Al Almacen."));
                }
                return response;
            }
        }

        public List<AlmacenDTO> getAlmacenInv(string id)
        {
            List<AlmacenDTO> lista = new List<AlmacenDTO>();
            var user = db.Invercionistas.Find(id);
            if (user != null) {
                var inver = db.AlmacenInversionistas.Where(ad => ad.InversionistaId == id).ToList<AlmacenInversionista>();
                if (inver != null)
                {
                    foreach (AlmacenInversionista item in inver)
                    {
                        Almacen a = db.Almacenes.Find(item.AlmacenId);
                        lista.Add(new AlmacenDTO
                        {
                            AlmacenId = a.AlmacenId,
                            Correo = a.Correo,
                            Direccion = a.Direccion,
                            Nombre = a.Nombre,
                            Telefono = a.Telefono
                        });
                    }

                }
            }
            
            return lista;
        }
    }
}

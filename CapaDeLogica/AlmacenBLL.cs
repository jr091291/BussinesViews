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
    public class AlmacenBLL
    {
        private Contexto db = new Contexto();

        public RespuestaDTO<AlmacenDTO> InsertarAlmacen(AlmacenDTO almacen)
        {
            using (db = new Contexto())
            {
                RespuestaDTO<AlmacenDTO> response = new RespuestaDTO<AlmacenDTO>();
                try
                {
                    // preparar el cliente para guardar
                    Almacen almacenDAL = new Almacen();
                    almacenDAL.Direccion = almacen.Direccion;
                    almacenDAL.Nombre = almacen.Nombre;
                    almacenDAL.Correo = almacen.Correo;
                    almacenDAL.Telefono = almacen.Telefono;

                    db.Almacenes.Add(almacenDAL);

                    // preparar la respuesta
                    response.Rows = db.SaveChanges();
                    response.Mensagge = "Se realizó la operación satisfactoriamente";
                    almacen.AlmacenId = almacenDAL.AlmacenId;
                    response.Data = almacen;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    response.Mensagge = "Se Ha Presentado Un Error Al Guardar El Almacen";
                    response.Errors.Add( new ResponseErrorDTO(ex.GetHashCode().ToString(), ex.Message));
                    return response;
                }
                catch (Exception ex)
                {
                    response.Mensagge = "Se Ha Presentado Un Error";
                    response.Errors.Add(new ResponseErrorDTO(ex.GetHashCode().ToString(), ex.Message));
                }
                return response;
            }
        }

        public async Task<RespuestaDTO<AlmacenDTO>> FindAlmacenById(int id)
        {
            using (db = new Contexto())
            {
                RespuestaDTO<AlmacenDTO> response = new RespuestaDTO<AlmacenDTO>();

                try
                {
                    var almacen =  await db.Almacenes.FindAsync(id);

                    if (almacen == null)
                    {
                        response.Mensagge = "Verifique el Numero De Identificacion Del Almacen";
                        response.Errors.Add(new ResponseErrorDTO { Code = "", Mensagge = "El Almacen No Se Encuentra Resgistrado." });
                        return response;
                    }
                    response.Mensagge = "Se Ha Encontrado El Almacen";
                    response.Data = new AlmacenDTO()
                    {
                        AlmacenId = almacen.AlmacenId,
                        Nombre = almacen.Nombre,
                        Correo = almacen.Correo,
                        Direccion = almacen.Direccion,
                        Telefono = almacen.Telefono
                    };
                    return response;
                }
                catch (Exception e)
                {
                    response.Mensagge = "Se Ha Presentado Un Error";
                    response.Errors.Add(new ResponseErrorDTO { Code = e.GetHashCode().ToString(), Mensagge = e.Message });
                    return response;
                }
            }
        }

        public List<Almacen> GetAlmacenes()
        {
            using (Contexto db = new Contexto())
            {
                return db.Almacenes
                    .Select(almacen =>
                        new Almacen
                        {
                            AlmacenId = almacen.AlmacenId,
                            Nombre = almacen.Nombre,
                            Direccion = almacen.Direccion,
                            Telefono = almacen.Telefono
                        }
                    ).ToList();
            }
        }
    }
}
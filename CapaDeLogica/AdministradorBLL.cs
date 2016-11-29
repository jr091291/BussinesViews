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
    public class AdministradorBLL
    {
        private Contexto db = new Contexto();

        public RespuestaDTO<AdministradorDTO> InsertarAdministrador(AdministradorDTO administrador)
        {
            using (db = new Contexto())
            {
                RespuestaDTO<AdministradorDTO> response = new RespuestaDTO<AdministradorDTO>();
                try
                {
                    // preparar el admin para guardar
                    Administrador admin = new Administrador();
                    admin.AdministradorId = administrador.UserId;
                    admin.AlmacenId = administrador.AlmacenId;

                    db.Administradores.Add(admin);

                    // preparar la respuesta
                    response.Rows = db.SaveChanges();
                    response.Mensagge = "Se Ha Agregado El Administrador Correctamente.";
                    response.Data = administrador;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    response.Mensagge = "Se Ha Presentado Un Error Al Guardar El Almacen";
                    response.Errors.Add(new ResponseErrorDTO(ex.GetHashCode().ToString(), ex.Message));
                    return response;
                }
                catch (Exception ex)
                {
                    response.Mensagge = "Se Ha Presentado Un Error";
                    response.Errors.Add(new ResponseErrorDTO(ex.GetHashCode().ToString(), ex.Message));
                    response.Errors.Add(new ResponseErrorDTO("", "El Admin Ya se Encuentre Registrado a este almacen."));
                }
                return response;
            }
        }

        public async Task<RespuestaDTO<AdministradorDTO>> FindByIdAdmin(string id)
        {
            using (db = new Contexto())
            {
                RespuestaDTO<AdministradorDTO> response = new RespuestaDTO<AdministradorDTO>();
                var admin = await db.Administradores.FindAsync(id);
                if (admin == null)
                {
                    response.Mensagge = "El Administrador No se Encuentra Registrado";
                    response.Errors.Add(new ResponseErrorDTO("", "Admin Inexistente"));
                    return response;
                }
                response.Mensagge = "Admin Encontrado";
                response.Data = new AdministradorDTO
                {
                    AlmacenId = admin.AlmacenId,
                    UserId = admin.AdministradorId,
                };
                return response;
            }
        }

        public List<AlmacenDTO> getAlmacenAdmin(string id)
        {
            List<AlmacenDTO> lista = new List<AlmacenDTO>();
            var admin = db.Administradores.Where(ad => ad.AdministradorId == id).ToList<Administrador>();
            if (admin != null)
            {
                foreach (Administrador administrador in admin) {
                    Almacen a = db.Almacenes.Find(administrador.AlmacenId);
                    lista.Add( new AlmacenDTO
                    {
                        AlmacenId = a.AlmacenId,
                        Correo = a.Correo,
                        Direccion = a.Direccion,
                        Nombre = a.Nombre,
                        Telefono = a.Telefono
                    });
                }
              
            }
            return lista;
        }
    }
}

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
    public class CierreBLL
    {

        Contexto db = new Contexto();

        public RespuestaDTO<CierreDTO> InsertarCierre(CierreDTO cierre)
        {
            using (db = new Contexto())
            {
                RespuestaDTO<CierreDTO> response = new RespuestaDTO<CierreDTO>();
                try
                {
                    // preparar el cliente para guardar
                    Cierre cierreDal = new Cierre();

                    var qCietre =  db.Cierres.Where(q => q.Fecha == cierre.Fecha && q.AlmacenId == cierre.AlmacenId).FirstOrDefault<Cierre>();

                    if (qCietre != null)
                    {
                        response.Mensagge = "Ya se Ha Registrado Un Cierre Para Esta Fecha Del Almacen";
                        response.Errors.Add(new ResponseErrorDTO("", "Fecha: " + qCietre.Fecha.ToString("dd/MM/yyyy") +  " Almacen Id: "+ qCietre.AlmacenId + " Ya Registra Un Cierre"));
                        return response;
                    }

                    cierreDal.Bancos = cierre.Bancos;
                    cierreDal.Efectivo = cierre.Efectivo;
                    cierreDal.AlmacenId = cierre.AlmacenId;
                    cierreDal.Facturas = cierre.Facturas;
                    cierreDal.Invercion = cierre.Costos;
                    cierreDal.Costos = cierre.Gastos;
                    cierreDal.Fecha = cierre.Fecha;
                  
                    db.Cierres.Add(cierreDal);

                    // preparar la respuesta
                    response.Rows = db.SaveChanges();
                    response.Mensagge = "Se Registro el Cierre satisfactoriamente";
                    response.Data = cierre;

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
                }
                return response;
            }
        }
    }
}

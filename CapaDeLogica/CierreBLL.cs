using CapaDeDatos;
using Entidades.DTO.Almacen;
using Entidades.DTO.reportes;
using Entidades.DTO.Reportes;
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

                    var qCietre = db.Cierres.Where(q => q.Fecha == cierre.Fecha && q.AlmacenId == cierre.AlmacenId).FirstOrDefault<Cierre>();

                    if (qCietre != null)
                    {
                        response.Mensagge = "Ya se Ha Registrado Un Cierre Para Esta Fecha Del Almacen";
                        response.Errors.Add(new ResponseErrorDTO("", "Fecha: " + qCietre.Fecha.ToString("dd/MM/yyyy") + " Almacen Id: " + qCietre.AlmacenId + " Ya Registra Un Cierre"));
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

        public RespuestaDTO<TotalVentasAlmacenesDTO> getVentas(SolicitudReporteDTO solicitud)
        {
            RespuestaDTO<TotalVentasAlmacenesDTO> response = new RespuestaDTO<TotalVentasAlmacenesDTO>();
            try {
                TotalVentasAlmacenesDTO totalVentas = new TotalVentasAlmacenesDTO();
                List<TotalVentasAlmacenDTO> ventas = new List<TotalVentasAlmacenDTO>();

                foreach (int idAlmacen in solicitud.ListadoAlmacenes)
                {
                    TotalVentasAlmacenDTO ventasAlmacen = new TotalVentasAlmacenDTO();
                    List<DatosVentasDto> cierres = new List<DatosVentasDto>();

                    var almacen = db.Almacenes.Find(idAlmacen);

                    ventasAlmacen.Almacen = new AlmacenDTO
                    {
                        AlmacenId = almacen.AlmacenId,
                        Correo = almacen.Correo,
                        Direccion = almacen.Direccion,
                        Nombre = almacen.Nombre,
                        Telefono = almacen.Telefono
                    };

                    var query = db.Cierres.Where(cierre => cierre.Fecha <= solicitud.FechaFin && cierre.Fecha >= solicitud.FechaIni && cierre.AlmacenId == idAlmacen).OrderBy(c=> c.Fecha).ToList<Cierre>();

                    foreach (Cierre c in query)
                    {
                        cierres.Add(new DatosVentasDto
                        {
                            Fecha = c.Fecha.ToShortDateString(),
                            Venta = c.Bancos + c.Efectivo,
                            Bancos = c.Bancos,
                            Efectivo = c.Efectivo,
                        });

                        ventasAlmacen.TotalBancos += c.Bancos;
                        ventasAlmacen.TotalEfectivo += c.Efectivo;
                        ventasAlmacen.TotalVentas += c.Bancos + c.Efectivo;
                    }
                    totalVentas.TotalBancos += ventasAlmacen.TotalBancos;
                    totalVentas.TotalEfectivo += ventasAlmacen.TotalEfectivo;
                    totalVentas.TotalVentas += ventasAlmacen.TotalVentas;
                    
                    ventasAlmacen.ventas = cierres;
                    ventas.Add(ventasAlmacen);
                }

                totalVentas.ventasPorAlmacen = ventas;
                response.Data = totalVentas;
                response.Mensagge = "Consulta Realizada Con Exito";
                return response;
            }
            catch (Exception e) {
                response.Mensagge = "Se Ha Presentado Un Error";
                response.Errors.Add(new ResponseErrorDTO("500",e.Message));
                return response;
            }
        }

        public RespuestaDTO<TotalCostosAlmacenesDTO> getCostos(SolicitudReporteDTO solicitud)
        {
            RespuestaDTO<TotalCostosAlmacenesDTO> response = new RespuestaDTO<TotalCostosAlmacenesDTO>();
            try
            {
                TotalCostosAlmacenesDTO totalCostos= new TotalCostosAlmacenesDTO();
                List<TotalCostosAlmacenDTO> costos = new List<TotalCostosAlmacenDTO>();

                foreach (int idAlmacen in solicitud.ListadoAlmacenes)
                {
                    TotalCostosAlmacenDTO costosAlmacen = new TotalCostosAlmacenDTO();
                    List<DatosCostosDTO> cierres = new List<DatosCostosDTO>();

                    var almacen = db.Almacenes.Find(idAlmacen);

                    costosAlmacen.Almacen = new AlmacenDTO
                    {
                        AlmacenId = almacen.AlmacenId,
                        Correo = almacen.Correo,
                        Direccion = almacen.Direccion,
                        Nombre = almacen.Nombre,
                        Telefono = almacen.Telefono
                    };

                    var query = db.Cierres.Where(cierre => cierre.Fecha <= solicitud.FechaFin && cierre.Fecha >= solicitud.FechaIni && cierre.AlmacenId == idAlmacen).OrderBy(c => c.Fecha).ToList<Cierre>();

                    foreach (Cierre c in query)
                    {
                        cierres.Add(new DatosCostosDTO
                        {
                            Fecha = c.Fecha.ToShortDateString(),
                            Facturas = c.Facturas,
                            Invercion = c.Invercion,
                            Total = c.Facturas + c.Invercion 
                        });

                        costosAlmacen.TotalInversion += c.Invercion;
                        costosAlmacen.TotalFacturas += c.Efectivo;
                        costosAlmacen.TotalCostos += c.Facturas + c.Invercion;
                    }
                    totalCostos.TotalInverciones += costosAlmacen.TotalInversion;
                    totalCostos.TotalFacturas += costosAlmacen.TotalFacturas;
                    totalCostos.TotalCostos += costosAlmacen.TotalCostos;

                    costosAlmacen.costos = cierres;
                    costos.Add(costosAlmacen);
                }

                totalCostos.costosPorAlmacen = costos;
                response.Data = totalCostos;
                response.Mensagge = "Consulta Realizada Con Exito";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagge = "Se Ha Presentado Un Error";
                response.Errors.Add(new ResponseErrorDTO("500", e.Message));
                return response;
            }
        }

        public RespuestaDTO<TotalGastosAlmacenes> getGastos(SolicitudReporteDTO solicitud)
        {
            RespuestaDTO<TotalGastosAlmacenes> response = new RespuestaDTO<TotalGastosAlmacenes>();
            try
            {
                TotalGastosAlmacenes totalGastos = new TotalGastosAlmacenes();
                List<TotalGastosAlmacen> gastos = new List<TotalGastosAlmacen>();

                foreach (int idAlmacen in solicitud.ListadoAlmacenes)
                {
                    TotalGastosAlmacen costosAlmacen = new TotalGastosAlmacen();
                    List<DatosGastosDTO> cierres = new List<DatosGastosDTO>();

                    var almacen = db.Almacenes.Find(idAlmacen);

                    costosAlmacen.Almacen = new AlmacenDTO
                    {
                        AlmacenId = almacen.AlmacenId,
                        Correo = almacen.Correo,
                        Direccion = almacen.Direccion,
                        Nombre = almacen.Nombre,
                        Telefono = almacen.Telefono
                    };

                    var query = db.Cierres.Where(cierre => cierre.Fecha <= solicitud.FechaFin && cierre.Fecha >= solicitud.FechaIni && cierre.AlmacenId == idAlmacen).OrderBy(c => c.Fecha).ToList<Cierre>();

                    foreach (Cierre c in query)
                    {
                        cierres.Add(new DatosGastosDTO
                        {
                            Fecha = c.Fecha.ToShortDateString(),
                            Gastos = c.Costos
                        });

                        costosAlmacen.TotalGastos += c.Costos;
                    }
                    totalGastos.TotalGastos += costosAlmacen.TotalGastos;
                   
                    costosAlmacen.Gastos = cierres;
                    gastos.Add(costosAlmacen);
                }

                totalGastos.CostosPorAlmacen = gastos;
                response.Data = totalGastos; 
                response.Mensagge = "Consulta Realizada Con Exito";
                return response;
            }
            catch (Exception e)
            {
                response.Mensagge = "Se Ha Presentado Un Error";
                response.Errors.Add(new ResponseErrorDTO("500", e.Message));
                return response;
            }
        }
    }
}

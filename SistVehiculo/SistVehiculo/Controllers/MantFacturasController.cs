using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantFacturasController : Controller
    {
        // GET: MantFacturas
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();

        public ActionResult ListaEncabezado(string num_factura = null, string estado = null)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaEncabezadoFactura_Result> modeloVista = new List<pa_RetornaEncabezadoFactura_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaEncabezadoFactura(num_factura, estado).ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        public ActionResult RetornaClientes()
        {
            List<pa_RetornaCliente_Result> clientes = this.modeloBD.pa_RetornaCliente("","","","").ToList();
            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaPlacaVehiculo(int idCliente)
        {

            List<pa_RetornaVehiculosxIDCliente_Result> vehiculos = this.modeloBD.pa_RetornaVehiculosxIDCliente(idCliente).ToList();
            return Json(vehiculos);
        }

        public ActionResult RetornaMarcaVehiculo(int idVehiculo)
        {
            List<pa_RetornaMarcaVehiculoxPlaca_Result> marcaVehiculo = this.modeloBD.pa_RetornaMarcaVehiculoxPlaca(idVehiculo).ToList();
            return Json(marcaVehiculo);
        }

        public ActionResult RetornaTipoVehiculo(string marcaVehiculo)
        {
            List<pa_RetornaTipoVehiculoxMarca_Result> tipoVehiculo = this.modeloBD.pa_RetornaTipoVehiculoxMarca(marcaVehiculo).ToList();
            return Json(tipoVehiculo);
        }

        public ActionResult RetornaClienteID(int idCliente)
        {
            List<pa_RetornaClienteID_Result> clienteID = this.modeloBD.pa_RetornaClienteID(idCliente).ToList();
            return Json(clienteID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaEstadoFactura()
        {
            List<pa_RetornaEstadoFactura_Result> estadoFactura = this.modeloBD.pa_RetornaEstadoFactura("").ToList();
            return Json(estadoFactura, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaServicioProducto()
        {
            List<pa_RetornaTipoServicioProducto_Result> servicioProducto = this.modeloBD.pa_RetornaTipoServicioProducto("","").ToList();
            return Json(servicioProducto, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaServicioProductoID(int idTipoServicioProducto)
        {
            List<pa_RetornaTipoServicioProductoID_Result> servicioProductoID = this.modeloBD.pa_RetornaTipoServicioProductoID(idTipoServicioProducto).ToList();
            return Json(servicioProductoID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertaEncabezado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertaEncabezadoFactura(string pNum_factura, DateTime pFecha, float pMontoTotal, int pEstado, int pIdCliente, int pIdVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaEncabezadoFactura(pNum_factura, pFecha, pMontoTotal, pEstado, pIdCliente, pIdVehiculo);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            /*Se ejecuta cuando haya o no haya un error, siempre se ejecutará*/
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Encabezado de factura ingresado";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el encabezado de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }

        public ActionResult ModificaEncabezadoFactura(int id_Factura)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idFactura
            pa_RetornaEncabezadoFacturaID_Result modeloVista = new pa_RetornaEncabezadoFacturaID_Result();
            modeloVista = this.modeloBD.pa_RetornaEncabezadoFacturaID(id_Factura).FirstOrDefault();

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificaEncabezadoFactura(int pId_factura, string pNum_factura, DateTime pFecha, float pMontoTotal, int pEstado, int pIdCliente, int pIdVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaEncabezadoFactura(pId_factura,pNum_factura, pFecha, pMontoTotal, pEstado, pIdCliente, pIdVehiculo);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            /*Se ejecuta cuando haya o no haya un error, siempre se ejecutará*/
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Encabezado de factura modificado";
                }
                else
                {
                    mensaje += ".No se pudo modificar el encabezado de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult ModificaEstadoOffEncabezadoFactura(int pId_factura)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaEstadoOffEncabezadoFactura(pId_factura);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            /*Se ejecuta cuando haya o no haya un error, siempre se ejecutará*/
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Encabezado de factura Inhabilitado";
                }
                else
                {
                    mensaje += ".No se pudo inhabilitar el encabezado de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult ModificaEstadoOnEncabezadoFactura(int pId_factura)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaEstadoOnEncabezadoFactura(pId_factura);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            /*Se ejecuta cuando haya o no haya un error, siempre se ejecutará*/
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Encabezado de factura Habilitado";
                }
                else
                {
                    mensaje += ".No se pudo habilitar el encabezado de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult InsertaDetalleFactura(string pNumFactura, int pTipoServicio, int pCantidad, float pPrecio)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaDetalleFactura(pNumFactura, pTipoServicio, pCantidad, pPrecio);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            ///Se ejecuta cuando haya o no haya un error, siempre se ejecutará
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = $"Detalle de factura ingresado";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el detalle de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult InsertaDetalleFacturaCliente(int pTipoServicio, int pIdCliente)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaServicioCliente(pTipoServicio, pIdCliente);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            ///Se ejecuta cuando haya o no haya un error, siempre se ejecutará
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = $"Detalle de factura ingresado al Cliente";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el detalle de factura al Cliente";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult InsertaDetalleFacturaVehiculo(int pTipoServicio, int pIdVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaServicioVehiculo(pTipoServicio, pIdVehiculo);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            ///Se ejecuta cuando haya o no haya un error, siempre se ejecutará
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = $"Detalle de factura ingresado al Vehiculo";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el detalle de factura al Vehiculo";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult ModificaMontoEncabezado(int pId_factura, float pMontoTotal)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaMontoEncabezadoFactura(pId_factura, pMontoTotal);
            }
            catch (Exception error)
            {
                mensaje = "Ocurrió un error: " + error.Message;

            }
            /*Se ejecuta cuando haya o no haya un error, siempre se ejecutará*/
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Monto de encabezado actualizado";
                }
                else
                {
                    mensaje += ".No se pudo actualizar el monto del encabezado";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult InsertaJsonPrueba(string pCodigo, float pPrecio)
        {
            string mensaje = "";

            mensaje = $" Código: {pCodigo}"+
                      $"Precio: {pPrecio}";

            return Json(new { resultado = mensaje });
        }
    }
}
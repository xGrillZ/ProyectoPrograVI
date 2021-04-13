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

        public ActionResult RetornaPlacaVehiculo(int idVehiculo)
        {

            List<pa_RetornaVehiculosxIDCliente_Result> placaVehiculo = this.modeloBD.pa_RetornaVehiculosxIDCliente(idVehiculo).ToList();
            return Json(placaVehiculo);
        }

        public ActionResult RetornaMarcaVehiculo(int idCliente)
        {
            List<pa_RetornaMarcaVehiculoxPlaca_Result> marcaVehiculo = this.modeloBD.pa_RetornaMarcaVehiculoxPlaca(idCliente).ToList();
            return Json(marcaVehiculo);
        }

        public ActionResult RetornaTipoVehiculo(int idTipoVehiculo)
        {
            List<pa_RetornaTipoVehiculoxMarca_Result> tipoVehiculo = this.modeloBD.pa_RetornaTipoVehiculoxMarca(idTipoVehiculo).ToList();
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

        bool verificaNumFactura(string pNumFactura, string idFactura)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string ced = pNumFactura;
                ///Resultado de la operación
                if (string.IsNullOrEmpty(idFactura))
                {
                    resultado = this.modeloBD.Factura.Count(Factura => Factura.num_factura == ced) <= 0;
                }
                else
                {
                    int cod2 = Convert.ToInt32(idFactura);
                    resultado = this.modeloBD.Factura.Count(Factura => Factura.num_factura == ced && Factura.id_factura != cod2) <= 0;
                }
            }
            catch
            {
                ///Mensaje de error
                string mensaje = "Error al verificar el número de factura";
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            ///Retorno del resultado
            return resultado;
        }

        public ActionResult InsertaEncabezado()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertaEncabezadoFacturas(string Num_factura, DateTime Fecha, float MontoTotal, int Estado, int IdCliente, int IdVehiculo, int IdTipoVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            if (this.verificaNumFactura(Num_factura, null))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaEncabezadoFactura(Num_factura, Fecha, MontoTotal, Estado, IdCliente, IdVehiculo, IdTipoVehiculo);
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
            }
            else
            {
                mensaje = "Este número de factura ya existe, debes ingresar otra";
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
        public ActionResult ModificaFactura(int Id_factura, string Num_factura, DateTime Fecha, float MontoTotal, int Estado, int IdCliente, int IdVehiculo, int idTipoVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            if (this.verificaNumFactura(Num_factura, Id_factura.ToString()))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_ModificaEncabezadoFactura(Id_factura, Num_factura, Fecha, MontoTotal, Estado, IdCliente, IdVehiculo, idTipoVehiculo);
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
            }
            else
            {
                mensaje = "Este número de factura ya existe, debes ingresar otra";
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

        public ActionResult EliminarEncabezadoFactura(int id_Factura)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idFactura
            pa_RetornaEncabezadoFacturaID_Result modeloVista = new pa_RetornaEncabezadoFacturaID_Result();
            modeloVista = this.modeloBD.pa_RetornaEncabezadoFacturaID(id_Factura).FirstOrDefault();

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminaFactura(int Id_factura)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaEncabezadoFactura(Id_factura);
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
                    mensaje = "Encabezado de factura eliminado";
                }
                else
                {
                    mensaje += ".No se pudo eliminar el encabezado de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }


        [HttpPost]
        public ActionResult InsertaDetalleFacturas(int num_factura, int idTipoServicioProducto, int cantidadServicioProducto, float Precio)
        {

            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaDetalleFactura(num_factura, idTipoServicioProducto, cantidadServicioProducto, Precio);
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
                    mensaje = "Detalle de factura ingresada";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el detalle de factura";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult InsertaDetalleFacturaCliente(int TipoServicio, int IdCliente, int IdClasificacion, int Cantidad, float Precio, float PrecioTotal)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaServicioCliente(TipoServicio, IdCliente, IdClasificacion, Cantidad, Precio, PrecioTotal);
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
                    mensaje = "Detalle de factura ingresado al Cliente";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el detalle de factura al Cliente";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult InsertaDetalleFacturaVehiculo(int TipoServicio, int IdVehiculo, int IdClasificacion, int Cantidad, float Precio, float PrecioTotal)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaServicioVehiculo(TipoServicio, IdVehiculo, IdClasificacion, Cantidad, Precio, PrecioTotal);
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
                    mensaje = "Detalle de factura ingresado al Vehiculo";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el detalle de factura al Vehiculo";
                }
            }

            return Json(new { resultado = mensaje });
        }

        [HttpPost]
        public ActionResult ModificaFacturaMontos(int idFactura, float montoTotal)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaMontoEncabezadoFactura(idFactura, montoTotal);
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
                    mensaje = "Monto Encabezado de factura modificado";
                }
                else
                {
                    mensaje += ".No se pudo modificar el monto de encabezado de factura";
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
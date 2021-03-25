using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantServiciosProductosController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantServiciosProductos
        public ActionResult ListaServiciosProductos()
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaTipoServicioProducto_Result> modeloVista = new List<pa_RetornaTipoServicioProducto_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaTipoServicioProducto("").ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }
        public ActionResult InsertarServiciosProductos()
        {
            AgregaClasificacionViewBag();
            return View();
        }

        /// <summary>
        /// Metodo para agregar las clasificaciones de tipos en un viewbag
        /// para que sean accedidas desde la vista. Es CASE SENSITIVE
        /// </summary>
        void AgregaClasificacionViewBag()
        {
            this.ViewBag.ListaClasificacion = this.modeloBD.pa_RetornaClasificacionSP("").ToList();
        }

        bool verificaServicio(string pCodigo, string pCodigoDos)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string cod = pCodigo;

                ///Resultado de la operación
                if (string.IsNullOrEmpty(pCodigoDos))
                {
                    ///Resultado de la operación
                    resultado = this.modeloBD.TipoServicioProducto.Count(tipoServicioProducto => tipoServicioProducto.codigo == cod) <= 0;
                }
                else
                {
                    int cod2 = Convert.ToInt32(pCodigoDos);
                    ///Resultado de la operación
                    resultado = this.modeloBD.TipoServicioProducto.Count(tipoServicioProducto => tipoServicioProducto.codigo == cod && tipoServicioProducto.idTipoServicioProducto != cod2) <= 0;
                }
            }
            catch
            {
                ///Mensaje de error
                string mensaje = "Error al verificar el código.";
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            ///Retorno del resultado
            return resultado;
        }

        [HttpPost]
        public ActionResult InsertarServiciosProductos(pa_RetornaTipoServicioProducto_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaServicio(modeloVista.codigo, null))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaTipoServicioProducto(modeloVista.codigo, modeloVista.descripcion,
                                                                                          modeloVista.precio, modeloVista.tipo);
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Registro insertado";
                    }
                    else
                    {
                        mensaje += ".No se pudo insertar";
                    }
                }
            }
            else
            {
                mensaje = "Esta código ya existe, debes ingresar otro.";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaClasificacionViewBag();
            return View();
        }

        public ActionResult ModificarServiciosProductos(int idTipoServicioProducto)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaTipoServicioProductoID_Result modeloVista = new pa_RetornaTipoServicioProductoID_Result();
            modeloVista = this.modeloBD.pa_RetornaTipoServicioProductoID(idTipoServicioProducto).FirstOrDefault();

            this.AgregaClasificacionViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarServiciosProductos(pa_RetornaTipoServicioProductoID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaServicio(modeloVista.codigo, modeloVista.idTipoServicioProducto.ToString()))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_ModificaTipoServicioProducto(modeloVista.idTipoServicioProducto, modeloVista.codigo, modeloVista.descripcion,
                                                                                          modeloVista.precio, modeloVista.tipo);
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Registro modificado";
                    }
                    else
                    {
                        mensaje += ".No se pudo modificar";
                    }
                }
            }
            else
            {
                mensaje = "Esta código ya existe, debes ingresar otro.";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaClasificacionViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        public ActionResult EliminarServiciosProductos(int idTipoServicioProducto)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idTipoServicioProducto
            pa_RetornaTipoServicioProductoID_Result modeloVista = new pa_RetornaTipoServicioProductoID_Result();
            modeloVista = this.modeloBD.pa_RetornaTipoServicioProductoID(idTipoServicioProducto).FirstOrDefault();

            this.AgregaClasificacionViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarServiciosProductos(pa_RetornaTipoServicioProductoID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaTipoServicioProducto(modeloVista.idTipoServicioProducto);
            }
            catch(Exception ex)
            {
                mensaje = "Ocurrió un error: " + ex.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Registro Eliminado";
                }
                else
                {
                    mensaje += ".No se pudo eliminar";
                }
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaClasificacionViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }
    }
}
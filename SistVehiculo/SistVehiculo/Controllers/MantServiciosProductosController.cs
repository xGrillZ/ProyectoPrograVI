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

        bool verificaServicio(string pCodigo)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string cod = pCodigo;
                ///Resultado de la operación
                resultado = this.modeloBD.TipoServicioProducto.Count(tipoServicioProducto => tipoServicioProducto.codigo == cod) <= 0;
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

            if (this.verificaServicio(modeloVista.codigo))
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

        public ActionResult ModificarServiciosProductos()
        {
            return View();
        }

        public ActionResult EliminarServiciosProductos()
        {
            return View();
        }
    }
}
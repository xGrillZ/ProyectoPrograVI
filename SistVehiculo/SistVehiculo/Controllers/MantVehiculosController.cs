using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantVehiculosController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantVehiculos
        public ActionResult ListaVehiculos()
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaVehiculos_Result> modeloVista = new List<pa_RetornaVehiculos_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaVehiculos("").ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        void AgregaTipoVehiculoViewBag()
        {
            this.ViewBag.ListaTipoVehiculo = this.modeloBD.pa_RetornaTiposVehiculo("","").ToList();
        }

        void AgregaMarcaVehiculoViewBag()
        {
            this.ViewBag.ListaMarcaVehiculo = this.modeloBD.pa_RetornaMarcaVehiculo("", "", "").ToList();
        }


        public ActionResult InsertarVehiculos()
        {
            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();
            return View();
        }
       /* bool verificaCodigo(string pCodigo, string pCodigoDos)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string cod = pCodigo;

                if (string.IsNullOrEmpty(pCodigoDos))
                {
                    ///Resultado de la operación
                    resultado = this.modeloBD.Vehiculos.Count(Vehiculos => Vehiculos.placa == cod) <= 0;
                }
                else
                {
                    int cod2 = Convert.ToInt32(pCodigoDos);
                    ///Resultado de la operación
                    resultado = this.modeloBD.Vehiculos.Count(Vehiculos => Vehiculos.placa == cod && Vehiculos.idVehiculos != cod2) <= 0;
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
        }*/ 

        /// <summary>
        /// Revisar errores
        /// </summary>
        /// <returns></returns>
/*
        [HttpPost]
        public ActionResult InsertarVehiculos(pa_RetornaVehiculos_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaVehiculos( 
                        modeloVista.placa,
                        modeloVista.numeroPuerta,
                        modeloVista.numeroRueda,
                        modeloVista.tipoVehiculo,
                        modeloVista.marca
                        );


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
                        mensaje += "No se pudo insertar";
                    }
                }
                       

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();

            return View();
        }

*/
        public ActionResult ModificarVehiculos()
        {
            return View();
        }

        public ActionResult EliminarVehiculos(int idVehiculos)
        {            
            pa_RetornaVehiculosID_Result modeloVista = new pa_RetornaVehiculosID_Result();
            modeloVista = this.modeloBD.pa_RetornaVehiculosID(idVehiculos).FirstOrDefault();

            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarVehiculos(pa_RetornaVehiculosID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaVehiculo(modeloVista.idVehiculos);
            }
            catch (Exception ex)
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

            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        public ActionResult RpServicioVehiculo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetornaServiciosVehiculoLista()
        {
            List<pa_RetornaServiciosVehiculo_Result> serviciosVehiculo =
                this.modeloBD.pa_RetornaServiciosVehiculo("","","").ToList();
            return Json(new { resultado = serviciosVehiculo });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantVehiculosClienteController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantVehiculosCliente
        public ActionResult ListaVehiculosCliente()
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaVehiculosxIDCliente_Result> modeloVista = new List<pa_RetornaVehiculosxIDCliente_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaVehiculosxIDCliente(null).ToList();

            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        void AgregaTipoVehiculoViewBag()
        {
            this.ViewBag.ListaTipoVehiculo = this.modeloBD.pa_RetornaTiposVehiculo("", "").ToList();
        }

        void AgregaMarcaVehiculoViewBag()
        {
            this.ViewBag.ListaMarcaVehiculo = this.modeloBD.pa_RetornaMarcaVehiculo("", "", "").ToList();
        }
        public ActionResult InsertarVehiculosCliente()
        {
            return View();
        }

        public ActionResult ModificarVehiculosCliente(int idVehiculosCliente)
        {
            pa_RetornaVehiculosClienteID_Result modeloVista = new pa_RetornaVehiculosClienteID_Result();
            modeloVista = this.modeloBD.pa_RetornaVehiculosClienteID(idVehiculosCliente).FirstOrDefault();

            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();

            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarVehiculosCliente(pa_RetornaVehiculosClienteID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaVehiculosCliente(
                    modeloVista.idVehiculosCliente,
                    modeloVista.placa,
                    modeloVista.tipoVehiculo,
                    modeloVista.marcaVehiculo,
                    modeloVista.numeroPuerta,
                    modeloVista.numeroRueda
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
                    mensaje = " Registro modificado";
                }
                else
                {
                    mensaje += " No se pudo modificar";
                }
            }


            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();

            return View(modeloVista);
        }

        public ActionResult EliminarVehiculosCliente(int idCliente)
        {
            pa_RetornaVehiculosClienteID_Result modeloVista = new pa_RetornaVehiculosClienteID_Result();
            modeloVista = this.modeloBD.pa_RetornaVehiculosClienteID(idCliente).FirstOrDefault();

            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarVehiculosCliente(pa_RetornaVehiculosClienteID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaVehiculosCliente(modeloVista.placa);
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
                    mensaje += " No se pudo eliminar";
                }
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        public ActionResult RpVehiculosCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetornaVehiculosClienteLista()
        {
            List<pa_RetornaVehiculosCliente_Result> listaVehiculosCliente = 
                this.modeloBD.pa_RetornaVehiculosCliente("","","","").ToList();
            return Json(new { resultado = listaVehiculosCliente });
        }
    }
}
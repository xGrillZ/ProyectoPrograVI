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
        public ActionResult ListaVehiculosCliente(string placa = null, string nombreCliente = null, string marca = null, string numCedula = null)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaVehiculosCliente_Result> modeloVista = new List<pa_RetornaVehiculosCliente_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaVehiculosCliente(placa, nombreCliente, marca, numCedula).ToList();

            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        public ActionResult InsertarVehiculosCliente()
        {
            return View();
        }

        public ActionResult RetornaClientes()
        {
            List<pa_RetornaCliente_Result> clientes = this.modeloBD.pa_RetornaCliente("", "", "", "").ToList();
            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaVehiculo()
        {
            List<pa_RetornaVehiculos_Result> vehiculos = this.modeloBD.pa_RetornaVehiculos("").ToList();
            return Json(vehiculos, JsonRequestBehavior.AllowGet);
        }

       /* public ActionResult RetornaTipoVehiculo(int idVehiculo)
        {
            List<pa_RetornaTipo>
            return Json(tipoVehiculo, JsonRequestBehavior.AllowGet);
        }*/

        [HttpPost]
        public ActionResult InsertarVehiculosCliente(int idVehiculo, int idCliente, int idTipoVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_InsertaVehiculosCliente(idVehiculo, idCliente, idTipoVehiculo);
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
                    mensaje = "Vehiculo de Cliente ingresado";
                }
                else
                {
                    mensaje += ".No se pudo ingresar el vehículo del cliente";
                }
            }

            return Json(new { resultado = mensaje });
        }
    }
}
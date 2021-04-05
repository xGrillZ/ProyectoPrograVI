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
        public ActionResult InsertarVehiculosCliente()
        {
            return View();
        }

        public ActionResult ModificarVehiculosCliente()
        {
            return View();
        }

        public ActionResult EliminarVehiculosCliente()
        {
            return View();
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
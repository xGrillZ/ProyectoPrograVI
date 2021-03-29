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
            return View();
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
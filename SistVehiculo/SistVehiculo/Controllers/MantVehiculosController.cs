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
            return View();
        }
        public ActionResult InsertarVehiculos()
        {
            return View();
        }

        public ActionResult ModificarVehiculos()
        {
            return View();
        }

        public ActionResult EliminarVehiculos()
        {
            return View();
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
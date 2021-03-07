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
        sistVehiculoEntities modeloBD = new sistVehiculoEntities();
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
    }
}
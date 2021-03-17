using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantMarcaVehiculosController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantMarcaVehiculos
        public ActionResult ListaMarcaVehiculos()
        {
            return View();
        }
        public ActionResult InsertarMarcaVehiculos()
        {
            return View();
        }

        public ActionResult ModificarMarcaVehiculos()
        {
            return View();
        }

        public ActionResult EliminarMarcaVehiculos()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantTipoVehiculosController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantTipoVehiculos
        public ActionResult ListaTipoVehiculos()
        {
            return View();
        }
        public ActionResult InsertarTipoVehiculos()
        {
            return View();
        }

        public ActionResult ModificarTipoVehiculos()
        {
            return View();
        }

        public ActionResult EliminarTipoVehiculos()
        {
            return View();
        }

    }
}
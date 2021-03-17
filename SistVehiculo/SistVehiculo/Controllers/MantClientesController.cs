using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantClientesController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantClientes
        public ActionResult ListaClientes()
        {
            return View();
        }
        public ActionResult InsertarClientes()
        {
            return View();
        }

        public ActionResult ModificarClientes()
        {
            return View();
        }

        public ActionResult EliminarClientes()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class ConsultoresController : Controller
    {
        sistVehiculoEntities modeloBD = new sistVehiculoEntities();
        // GET: Consultores

        public ActionResult ListaClientes()
        {
            return View();
        }
        public ActionResult ModificarClientes()
        {
            return View();
        }
    }
}
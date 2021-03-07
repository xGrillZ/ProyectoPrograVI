using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantServiciosProductosController : Controller
    {
        sistVehiculoEntities modeloBD = new sistVehiculoEntities();
        // GET: MantServiciosProductos
        public ActionResult ListaServiciosProductos()
        {
            return View();
        }
        public ActionResult InsertarServiciosProductos()
        {
            return View();
        }

        public ActionResult ModificarServiciosProductos()
        {
            return View();
        }

        public ActionResult EliminarServiciosProductos()
        {
            return View();
        }
    }
}
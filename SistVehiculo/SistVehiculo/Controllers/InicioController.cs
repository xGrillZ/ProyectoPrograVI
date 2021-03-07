using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class InicioController : Controller
    {
        /// <summary>
        /// Instancia del modelo de base de datos
        /// </summary>
        sistVehiculoEntities modeloBD = new sistVehiculoEntities();

        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }
    }
}
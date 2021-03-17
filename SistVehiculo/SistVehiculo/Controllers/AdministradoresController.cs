using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class AdministradoresController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: Administradores
        public ActionResult Index()
        {
            return View();
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVehiculo.Controllers
{
    public class SesionController : Controller
    {
        // GET: Sesion
        public ActionResult Logout()
        {
            ///Cerrar la sesión
            Session["usuariologueado"] = null;
            ///Retorna a la vista de logeo
            return RedirectToAction("Inicio","Inicio");
        }
    }
}
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

        [HttpPost]
        public ActionResult Inicio(string correoElectronico, string password)
        {
            pa_RetornaClienteCorreoPwd_Result resultadoSp = this.modeloBD.pa_RetornaClienteCorreoPwd(correoElectronico, password).FirstOrDefault();

            try
            {
                if (resultadoSp == null)
                {
                    ViewBag.Error = "Correo electrónico o Contraseña inválida";
                    return View();
                }
                else
                {
                    Session["User"] = resultadoSp;
                }

                return RedirectToAction("Principal", "Inicio");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Principal()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class SesionController : Controller
    {
        /// <summary>
        /// Instancia del modelo de base de datos
        /// </summary>
        sistVehiculoEntities modeloBD = new sistVehiculoEntities();

        // GET: Sesion
        public ActionResult Logout()
        {

            this.actualizaUltimaSesion();
            ///Cerrar la sesión
            this.Session.Add("idusuario", null);
            this.Session.Add("tipousuario", null);
            this.Session.Add("usuariologueado", null);
            ///Retorna a la vista de logeo
            return RedirectToAction("Inicio","Inicio");
        }

        void actualizaUltimaSesion()
        {
            ///Variable que almacena el IDUsuario a la hora de iniciar sesión
            int dataUser = int.Parse(Session["idusuario"].ToString());

            ///Variable fecha actual
            string fechaActual = DateTime.Now.ToString();

            int cantRegistrosAfectados = 0;

            string mensaje = "";
            
            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaUltimaSesionCliente(dataUser, DateTime.Parse(fechaActual));
            }
            catch (Exception capturaExcepcion)
            {
                mensaje += $"Ocurrió un error: {capturaExcepcion}";
                ///Mensaje de excepcion
                Response.Write("<script>alert('" + mensaje + "')</script>");
            }
            finally{
                if (cantRegistrosAfectados > 0)
                {
                    mensaje = "Cierre de sesión exitosa";
                }
                else
                {
                    mensaje += ".No se pudo cerrar sesión";
                }
            }
        }
    }
}
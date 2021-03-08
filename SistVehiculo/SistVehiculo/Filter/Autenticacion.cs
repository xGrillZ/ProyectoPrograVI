using SistVehiculo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVehiculo.Filter
{
    public class Autenticacion : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                /*Filtro para evitar que salten páginas*/
                if (HttpContext.Current.Session["usuariologueado"] == null)
                {
                    ///Inicio/Inicio es una vista que no requiere sesión
                    ///Si se ingresa a un vista de controlador que no sea InicioController
                    ///y no está con la sesión activa no lo dejará ingresar a la vista a entrar
                    if (filterContext.Controller is InicioController == false)
                    {
                        ///Redirección de vista, si se ingresa a otra vista sin haber logeado
                        filterContext.HttpContext.Response.Redirect("/Inicio/Inicio");
                    }
                }
                else
                {
                    ///Una vez iniciada la sesión, no se podrá devolver a la vista
                    ///Inicio/Inicio, ya que cuenta con una sesión iniciada
                    if (filterContext.Controller is InicioController == true)
                    {
                        ///Redirección de vista si se ingresa a la vista Inicio/Inicio
                        filterContext.HttpContext.Response.Redirect("/Home/Inicio");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
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
                if (HttpContext.Current.Session["User"] == null)
                {
                    /*Inicio/Inicio es una vista que no requiere sesión*/
                    if (filterContext.Controller is InicioController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Inicio/Inicio");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}
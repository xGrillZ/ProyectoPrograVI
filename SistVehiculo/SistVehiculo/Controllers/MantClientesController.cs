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
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaCliente_Result> modeloVista = new List<pa_RetornaCliente_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaCliente("", "", "", "").ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }
        public ActionResult InsertarClientes()
        {
            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            return View();
        }

        /// <summary>
        /// Metodo para agregar los generos en un viewbag
        /// para que sean accedidas desde la vista. Es CASE SENSITIVE
        /// </summary>
        void AgregaGeneroViewBag()
        {
            this.ViewBag.ListaGeneros = this.modeloBD.pa_RetornaGenero("").ToList();
        }

        /// <summary>
        /// Metodo para agregar los tipos de cliente en un viewbag
        /// para que sean accedidas desde la vista. Es CASE SENSITIVE
        /// </summary>
        void AgregaTipoClienteViewBag()
        {
            this.ViewBag.ListaTiposCliente = this.modeloBD.pa_RetornaTipoCliente("").ToList();
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
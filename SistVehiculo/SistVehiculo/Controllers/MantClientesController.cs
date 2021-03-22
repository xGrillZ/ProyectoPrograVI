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

        /// <summary>
        /// Retorna todas las provincias
        /// </summary>
        /// <returns></returns>
        public ActionResult RetornaProvincias()
        {
            List<RetornaProvincias_Result> provincias = this.modeloBD.RetornaProvincias(null).ToList();
            return Json(provincias);
        }

        /// <summary>
        /// Retorna todas los cantones según el ID provincia
        /// </summary>
        /// <returns></returns>
        public ActionResult RetornaCantones(int id_Provincia)
        {
            List<RetornaCantones_Result> cantones = this.modeloBD.RetornaCantones(null, id_Provincia).ToList();
            return Json(cantones);
        }

        /// <summary>
        /// Retorna todos los distritos según el ID del canton
        /// </summary>
        /// <param name="id_Canton"></param>
        /// <returns></returns>
        public ActionResult RetornaDistritos(int id_Canton)
        {
            List<RetornaDistritos_Result> distritos = this.modeloBD.RetornaDistritos(null, id_Canton).ToList();
            return Json(distritos);
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
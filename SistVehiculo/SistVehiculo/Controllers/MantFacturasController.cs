using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantFacturasController : Controller
    {
        // GET: MantFacturas
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        public ActionResult ListaEncabezado(string num_factura = null, string estado = null)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaEncabezadoFactura_Result> modeloVista = new List<pa_RetornaEncabezadoFactura_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaEncabezadoFactura(num_factura, estado).ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }
    }
}
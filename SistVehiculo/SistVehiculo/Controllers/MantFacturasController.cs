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

        public ActionResult RetornaClientes()
        {
            List<pa_RetornaCliente_Result> clientes = this.modeloBD.pa_RetornaCliente("","","","").ToList();
            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaPlacaVehiculo(int idCliente)
        {

            List<pa_RetornaVehiculosxIDCliente_Result> vehiculos = this.modeloBD.pa_RetornaVehiculosxIDCliente(idCliente).ToList();
            return Json(vehiculos);
        }

        public ActionResult RetornaMarcaVehiculo(string placa)
        {
            List<pa_RetornaMarcaVehiculoxPlaca_Result> marcaVehiculo = this.modeloBD.pa_RetornaMarcaVehiculoxPlaca(placa).ToList();
            return Json(marcaVehiculo);
        }

        public ActionResult RetornaTipoVehiculo(string marcaVehiculo)
        {
            List<pa_RetornaTipoVehiculoxMarca_Result> tipoVehiculo = this.modeloBD.pa_RetornaTipoVehiculoxMarca(marcaVehiculo).ToList();
            return Json(tipoVehiculo);
        }

        public ActionResult RetornaClienteID(int idCliente)
        {
            List<pa_RetornaClienteID_Result> clienteID = this.modeloBD.pa_RetornaClienteID(idCliente).ToList();
            return Json(clienteID);
        }

        public ActionResult InsertaEncabezado()
        {
            return View();
        }
    }
}
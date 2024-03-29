﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantVehiculosClienteController : Controller
    {
        sistvehiculoviEntities1 modeloBD = new sistvehiculoviEntities1();
        // GET: MantVehiculosCliente
        public ActionResult ListaVehiculosCliente(string placa = null, string nombreCliente = null, string marca = null, string numCedula = null)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaVehiculosCliente_Result> modeloVista = new List<pa_RetornaVehiculosCliente_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaVehiculosCliente(placa, nombreCliente, marca, numCedula).ToList();

            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        public ActionResult InsertarVehiculosCliente()
        {
            return View();
        }

        void AgregaTipoVehiculoViewBag()
        {
            this.ViewBag.ListaTipoVehiculo = this.modeloBD.pa_RetornaTiposVehiculo("", "").ToList();
        }

        void AgregaMarcaVehiculoViewBag()
        {
            this.ViewBag.ListaMarcaVehiculo = this.modeloBD.pa_RetornaMarcaVehiculo("", "", "").ToList();
        }

        public ActionResult RetornaClientes()
        {
            List<pa_RetornaCliente_Result> clientes = this.modeloBD.pa_RetornaCliente("", "", "", "").ToList();
            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaVehiculo()
        {
            List<pa_RetornaVehiculos_Result> vehiculos = this.modeloBD.pa_RetornaVehiculos("").ToList();
            return Json(vehiculos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RetornaTipoVehiculo(int idVehiculo)
        {
            List<pa_RetornaTiposVehiculoCliente_Result> tipoVehiculo = this.modeloBD.pa_RetornaTiposVehiculoCliente(null, null, idVehiculo).ToList();
            return Json(tipoVehiculo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertarVehiculosCliente(int idVehiculo, int idCliente, int idTipoVehiculo)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            if (this.verificarVehiculo(idVehiculo.ToString(), idCliente.ToString()))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaVehiculosCliente(idVehiculo, idCliente, idTipoVehiculo);
                }
                catch (Exception error)
                {
                    mensaje = "Ocurrió un error: " + error.Message;

                }
                /*Se ejecuta cuando haya o no haya un error, siempre se ejecutará*/
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Vehiculo de Cliente ingresado";
                    }
                    else
                    {
                        mensaje += ".No se pudo ingresar el vehículo del cliente";
                    }
                }
            }
            else
            {
                mensaje += "Ya tienes registrado este vehículo, ingresa otro.";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            return View();
        }

        bool verificarVehiculo(string idVehiculo, string idCliente)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                int idVehiculoCliente = Convert.ToInt32(idVehiculo);
                int idVehiculoCliente2 = Convert.ToInt32(idCliente);
                ///Resultado de la operación
                if (!string.IsNullOrEmpty(idCliente))
                {
                    resultado = this.modeloBD.VehiculosCliente.Count(VehiculoCliente => VehiculoCliente.idVehiculo == idVehiculoCliente && VehiculoCliente.idCliente == idVehiculoCliente2) <= 0;
                }
                else
                {
                    resultado = this.modeloBD.VehiculosCliente.Count(VehiculoCliente => VehiculoCliente.idVehiculo == idVehiculoCliente && VehiculoCliente.idCliente != idVehiculoCliente2) <= 0;
                }
            }
            catch
            {
                ///Mensaje de error
                string mensaje = "Error al verificar la cédula.";
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            ///Retorno del resultado
            return resultado;
        }

        public ActionResult ModificarVehiculosCliente(int idVehiculosCliente)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaVehiculosClienteID_Result modeloVista = new pa_RetornaVehiculosClienteID_Result();
            modeloVista = this.modeloBD.pa_RetornaVehiculosClienteID(idVehiculosCliente).FirstOrDefault();

            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarVehiculosCliente(pa_RetornaVehiculosClienteID_Result modeloVista)
        {

            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificarVehiculo(modeloVista.idVehiculo.ToString(), modeloVista.idCliente.ToString()))
            {
                try
                {

                    cantRegistrosAfectados = this.modeloBD.pa_ModificaVehiculosCliente(modeloVista.idVehiculosCliente, modeloVista.idVehiculo, modeloVista.idCliente, modeloVista.tipoVehiculo);
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Registro Modificado";
                    }
                    else
                    {
                        mensaje += ".No se pudo modificar";
                    }
                }
            }
            else
            {
                mensaje = "Este vehiculo ya existe en tu cuenta, debes ingresar otra";
            }
            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            return View(modeloVista);
        }

        public ActionResult RpVehiculosCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetornaVehiculosClienteLista()
        {
            List<pa_RetornaVehiculosCliente_Result> vehiculoCliente =
                this.modeloBD.pa_RetornaVehiculosCliente("","","","").ToList();
            return Json(new { resultado = vehiculoCliente });
        }

        public ActionResult EliminarVehiculosCliente(int idVehiculosCliente)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaVehiculosClienteID_Result modeloVista = new pa_RetornaVehiculosClienteID_Result();
            modeloVista = this.modeloBD.pa_RetornaVehiculosClienteID(idVehiculosCliente).FirstOrDefault();

            ///Enviar el modelo a la vista
            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarVehiculosCliente(pa_RetornaVehiculosClienteID_Result modeloVista)
        {

            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";
                try
                {

                    cantRegistrosAfectados = this.modeloBD.pa_EliminaVehiculosCliente(modeloVista.idVehiculosCliente);
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Registro Eliminado";
                    }
                    else
                    {
                        mensaje += ".No se pudo Eliminado";
                    }
                }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            ///Enviar el modelo a la vista
            AgregaTipoVehiculoViewBag();
            AgregaMarcaVehiculoViewBag();
            return View(modeloVista);
        }

        public ActionResult RpServiciosCliente()
        {
            return View();
        }



        [HttpPost]
        public ActionResult RetornaServiciosClienteLista()
        {
            List<pa_RetornaServiciosCliente_Result> serviciosCliente =
                this.modeloBD.pa_RetornaServiciosCliente("", "", "").ToList();

            return Json(new { resultado = serviciosCliente });
        }       

       

        public ActionResult RpVehiculosClienteConsultor()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RetornaVehiculosClienteConsutorLista()
        {
            int dataUser = int.Parse(Session["idusuario"].ToString());
            
            List<pa_RetornaVehiculosClienteIdConsutorID_Result> vehiculoCliente =
                this.modeloBD.pa_RetornaVehiculosClienteIdConsutorID(dataUser).ToList();
            return Json(new { resultado = vehiculoCliente });
          
        }


    }
}
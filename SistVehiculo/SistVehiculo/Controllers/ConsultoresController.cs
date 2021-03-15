using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class ConsultoresController : Controller
    {
        sistVehiculoEntities modeloBD = new sistVehiculoEntities();
        // GET: Consultores

        public ActionResult ModificaCliente(string numCedula) 
        {

            ///Obtener el registro que se desea modificar
            ///utilizando el parametro del metodo numCedula
            pa_RetornaCliente_Result modeloVista = new pa_RetornaCliente_Result();
            modeloVista = this.modeloBD.pa_RetornaCliente(numCedula).FirstOrDefault();
            //this.AgregaProvinciasViewBag();
            ///Envia el modelo a la vista            
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificaCliente(pa_RetornaCliente_Result modeloVista)
        {

            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error

            int cantRegistosAfectados = 0;
            string resultado = "";
            try
            {
                cantRegistosAfectados = this.modeloBD.pa_ModificaCliente(
                    modeloVista.nomCliente,
                    modeloVista.ape1Cliente,
                    modeloVista.ape2Cliente,
                    modeloVista.numCedula,
                    modeloVista.genero,
                    modeloVista.provincia,
                    modeloVista.fechNacimiento,
                    modeloVista.canton,
                    modeloVista.distrito,
                    modeloVista.email,
                    modeloVista.pTelefono);
            }
            catch (Exception error)
            {

                resultado = "Ocurrio un error" + error.Message;
            }
            finally
            {
                if (cantRegistosAfectados > 0)
                {
                    resultado = "Registro Modificado";
                }
                else
                {
                    resultado += "Mo se pudo modificar";
                }
            }
            Response.Write("<script language=javascript>alert('" + resultado + "');</script>");

            /*this.AgregaProvinciasViewBag();
            return View(modeloVista);*/

        }
    }
}
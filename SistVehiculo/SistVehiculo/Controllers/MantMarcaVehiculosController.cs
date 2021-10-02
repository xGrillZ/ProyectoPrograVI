using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantMarcaVehiculosController : Controller
    {
        sistvehiculoviEntities1 modeloBD = new sistvehiculoviEntities1();
        // GET: MantMarcaVehiculos
        public ActionResult ListaMarcaVehiculos(string codigo, string marca, string pais)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaMarcaVehiculo_Result> modeloVista = new List<pa_RetornaMarcaVehiculo_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaMarcaVehiculo(codigo, marca, pais).ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        public ActionResult InsertarMarcaVehiculos()
        {
            AgregaPaisViewBag();
            return View();
        }


        /// <summary>
        /// Metodo para agregar los paises en un viewbag
        /// para que sean accedidas desde la vista. Es CASE SENSITIVE
        /// </summary>
        void AgregaPaisViewBag()
        {
            this.ViewBag.ListaPaises = this.modeloBD.pa_RetornaPaisFabricante("").ToList();
        }

        bool verificaCodigo(string pCodigo, string pCodigoDos)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string cod = pCodigo;
                ///Resultado de la operación
                if (string.IsNullOrEmpty(pCodigoDos))
                {
                    resultado = this.modeloBD.MarcaVehiculo.Count(MarcaVehiculo => MarcaVehiculo.codigo == cod) <= 0;
                }
                else
                {
                    int cod2 = Convert.ToInt32(pCodigoDos);
                    resultado = this.modeloBD.MarcaVehiculo.Count(MarcaVehiculo => MarcaVehiculo.codigo == cod && MarcaVehiculo.idMarcaVehiculo != cod2) <= 0;
                }
            }
            catch
            {
                ///Mensaje de error
                string mensaje = "Error al verificar el código.";
                Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");
            }
            ///Retorno del resultado
            return resultado;
        }

        [HttpPost]
        public ActionResult InsertarMarcaVehiculos(pa_RetornaMarcaVehiculo_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCodigo(modeloVista.codigo, null))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaMarcaVehiculo(modeloVista.codigo, modeloVista.idPaisFabricante, modeloVista.marca);
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Registro insertado";
                    }
                    else
                    {
                        mensaje += ".No se pudo insertar";
                    }
                }
            }
            else
            {
                mensaje = "Este código ya existe, debes ingresar otra";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaPaisViewBag();
            return View();
        }

        public ActionResult ModificarMarcaVehiculos(int idMarcaVehiculo)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaMarcaVehiculoID_Result modeloVista = new pa_RetornaMarcaVehiculoID_Result();
            modeloVista = this.modeloBD.pa_RetornaMarcaVehiculoID(idMarcaVehiculo).FirstOrDefault();

            this.AgregaPaisViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarMarcaVehiculos(pa_RetornaMarcaVehiculoID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCodigo(modeloVista.codigo, modeloVista.idMarcaVehiculo.ToString()))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_ModificaMarcaVehiculo(modeloVista.idMarcaVehiculo, modeloVista.codigo, modeloVista.idPaisFabricante, modeloVista.marca);
                }
                catch (Exception ex)
                {
                    mensaje = "Ocurrió un error: " + ex.Message;
                }
                finally
                {
                    if (cantRegistrosAfectados > 0)
                    {
                        mensaje = "Registro modificado";
                    }
                    else
                    {
                        mensaje += ".No se pudo modificar";
                    }
                }
            }
            else
            {
                mensaje = "Esta código ya existe, debes ingresar otro.";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaPaisViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        public ActionResult EliminarMarcaVehiculos(int idMarcaVehiculo)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaMarcaVehiculoID_Result modeloVista = new pa_RetornaMarcaVehiculoID_Result();
            modeloVista = this.modeloBD.pa_RetornaMarcaVehiculoID(idMarcaVehiculo).FirstOrDefault();

            this.AgregaPaisViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarMarcaVehiculos(pa_RetornaMarcaVehiculoID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaMarcaVehiculo(modeloVista.idMarcaVehiculo);
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
                    mensaje += ".No se pudo eliminar";
                }
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaPaisViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }
    }
}
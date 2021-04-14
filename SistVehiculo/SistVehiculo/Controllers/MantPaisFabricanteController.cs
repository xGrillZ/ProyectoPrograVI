using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantPaisFabricanteController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantMarcaVehiculos
        public ActionResult ListaFabricante(string pais)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaPaisFabricante_Result> modeloVista = new List<pa_RetornaPaisFabricante_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaPaisFabricante(pais).ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        public ActionResult InsertarFabricante()
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
        #region VerificaCodigo
        bool verificaCodigo(string pCodigo, string pIdPaisFabricante)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string cod = pCodigo;
                ///Resultado de la operación
                if (string.IsNullOrEmpty(pIdPaisFabricante))
                {
                    resultado = this.modeloBD.PaisFabricante.Count(PaisFabricante => PaisFabricante.codigo == cod) <= 0;
                }
                else
                {
                    int cod2 = Convert.ToInt32(pIdPaisFabricante);
                    resultado = this.modeloBD.PaisFabricante.Count(PaisFabricante => PaisFabricante.codigo == cod && PaisFabricante.idFabricante != cod2) <= 0;
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
        #endregion

        [HttpPost]
        public ActionResult InsertarFabricante(pa_RetornaPaisFabricante_Result modeloVista)
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
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaPaisFabricante( modeloVista.codigo, modeloVista.pais);
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

        public ActionResult ModificarFabricante(int idFabricante)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaPaisFabricanteID_Result modeloVista = new pa_RetornaPaisFabricanteID_Result();
            modeloVista = this.modeloBD.pa_RetornaPaisFabricanteID(idFabricante).FirstOrDefault();

            this.AgregaPaisViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarFabricante(pa_RetornaPaisFabricanteID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCodigo(modeloVista.codigo, modeloVista.idFabricante.ToString()))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_ModificaPaisFabricante(modeloVista.idFabricante, modeloVista.codigo, modeloVista.pais);
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

        public ActionResult EliminarFabricante(int idFabricante)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaPaisFabricanteID_Result modeloVista = new pa_RetornaPaisFabricanteID_Result();
            modeloVista = this.modeloBD.pa_RetornaPaisFabricanteID(idFabricante).FirstOrDefault();

            this.AgregaPaisViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarFabricante(pa_RetornaPaisFabricanteID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaFabricante(modeloVista.idFabricante);
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
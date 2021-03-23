using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantTipoVehiculosController : Controller
    {
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantTipoVehiculos
        public ActionResult ListaTipoVehiculos()
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaTiposVehiculo_Result> modeloVista = new List<pa_RetornaTiposVehiculo_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaTiposVehiculo("","").ToList();
            ///Enviar a la vista el modelo
            return View(modeloVista);
        }

        public ActionResult InsertarTipoVehiculos()
        {
            return View();
        }

        bool verificaCodigo(string pCodigo)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string cod = pCodigo;
                ///Resultado de la operación
                resultado = this.modeloBD.TiposVehiculo.Count(TiposVehiculo => TiposVehiculo.codigo == cod) <= 0;
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
        public ActionResult InsertarTipoVehiculos(pa_RetornaTiposVehiculo_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCodigo(modeloVista.codigo))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_InsertaTiposVehiculo(modeloVista.codigo, modeloVista.tipo);
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
                mensaje = "Este código ya existe, debes ingresar otro";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            return View();
        }

        public ActionResult ModificarTipoVehiculos(int idTipoVehiculo)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaTiposVehiculoID_Result modeloVista = new pa_RetornaTiposVehiculoID_Result();
            modeloVista = this.modeloBD.pa_RetornaTiposVehiculoID(idTipoVehiculo).FirstOrDefault();

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        /*      [HttpPost]
              public ActionResult ModificarTipoVehiculos(pa_RetornaTiposVehiculoID_Result modeloVista)
              {
                  ///Variable que registra la cantidad de registros afectados
                  ///si un procedimiento que ejecuta insert, update o delete
                  ///no afecta registros implica que hubo un error
                  int cantRegistrosAfectados = 0;
                  string mensaje = "";

                  if (modeloVista.codigo == modeloVista.codigo)
                  {
                      try
                      {
                          cantRegistrosAfectados = this.modeloBD.pa_ModificaTiposVehiculo(modeloVista.idTipoVehiculo, modeloVista.codigo, modeloVista.tipo);
                      }
                      catch (Exception ex)
                      {
                          mensaje = "Ocurrió un error: " + ex.Message;
                      }
                      finally{

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
                      if (this.verificaCodigo(modeloVista.codigo))
                      {
                          try
                          {
                              cantRegistrosAfectados = this.modeloBD.pa_ModificaTiposVehiculo(modeloVista.idTipoVehiculo, modeloVista.codigo, modeloVista.tipo);
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
                  }

                  Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

                  ///Enviar el modelo a la vista
                  return View(modeloVista);
              }*/

        [HttpPost]
        public ActionResult ModificarTipoVehiculos(pa_RetornaTiposVehiculoID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCodigo(modeloVista.codigo))
            {
                try
                {
                    cantRegistrosAfectados = this.modeloBD.pa_ModificaTiposVehiculo(modeloVista.idTipoVehiculo, modeloVista.codigo, modeloVista.tipo);
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

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        public ActionResult EliminarTipoVehiculos(int idTipoVehiculo)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaTiposVehiculoID_Result modeloVista = new pa_RetornaTiposVehiculoID_Result();
            modeloVista = this.modeloBD.pa_RetornaTiposVehiculoID(idTipoVehiculo).FirstOrDefault();

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarTipoVehiculos(pa_RetornaTiposVehiculoID_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaTiposVehiculo(modeloVista.idTipoVehiculo);
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

            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SistVehiculo.Models;

namespace SistVehiculo.Controllers
{
    public class MantClientesController : Controller
    {
        /// <summary>
        /// Creación de la instancia del modelo BD
        /// </summary>
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();
        // GET: MantClientes
        public ActionResult ListaClientes(string numCedula = null, string nomCliente = null, string ape1Cliente = null, string ape2Cliente = null)
        {
            ///Variable que contiene los registros obtenidos
            List<pa_RetornaCliente_Result> modeloVista = new List<pa_RetornaCliente_Result>();
            ///Asígnación a la variable el resultado de la invocación del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaCliente(numCedula, nomCliente, ape1Cliente, ape2Cliente).ToList();
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

        bool verificaCedula(string pNumCedula, string idCliente)
        {
            ///Resultado de la operación
            bool resultado = true;
            try
            {
                ///Variable que almacenará el dato solicitado
                string ced = pNumCedula;
                ///Resultado de la operación
                if (string.IsNullOrEmpty(idCliente))
                {
                    resultado = this.modeloBD.Cliente.Count(Cliente => Cliente.numCedula == ced) <= 0;
                }
                else
                {
                    int cod2 = Convert.ToInt32(idCliente);
                    resultado = this.modeloBD.Cliente.Count(Cliente => Cliente.numCedula == ced && Cliente.idCliente != cod2) <= 0;
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

        [HttpPost]
        public ActionResult InsertarClientes(pa_RetornaCliente_Result modeloVista)
        {
            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";
            DateTime fechaActual = DateTime.Now;

            if (this.verificaCedula(modeloVista.numCedula, null))
            {
                try
                {
                    string contrasenaEncriptada = this.GetSHA256(modeloVista.contrasena);

                    cantRegistrosAfectados = this.modeloBD.pa_InsertaCliente(modeloVista.nomCliente, modeloVista.ape1Cliente, modeloVista.ape2Cliente,
                                                                                 modeloVista.numCedula, modeloVista.genero, modeloVista.provincia,
                                                                                 modeloVista.fechNacimiento, modeloVista.canton, modeloVista.distrito,
                                                                                 modeloVista.email, modeloVista.pTelefono, modeloVista.tipoCliente,
                                                                                 fechaActual, contrasenaEncriptada);
                    this.correoElectronicoIngreso(modeloVista.ape1Cliente, modeloVista.ape2Cliente, modeloVista.nomCliente,
                                                  modeloVista.email, fechaActual);
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
                mensaje = "Esta cédula ya existe, debes ingresar otra";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            return View();
        }

        void correoElectronicoIngreso(string pPriApellido, string pSecApellido, string pNombre, string pCorreo, DateTime pUltimoIngreso)
        {
            ///Creación de variables con datos
            string emailOrigen = "segurosxxiumca@gmail.com";
            string emailDestino = pCorreo;
            string contrasena = "CastroCarazoProgra";
            ///Creación del cuerpo del mensaje
            MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, "SmartVehicle",
                                                       $"Estimado cliente: {pPriApellido} {pSecApellido}" +
                                                       $" {pNombre}, gracias por confiar en SmartVehicle."+
                                                       $"Para nosotros es un placer servirle");
            oMailMessage.IsBodyHtml = true;
            ///Host
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contrasena);
            ///Enviar mensaje
            oSmtpClient.Send(oMailMessage);
            ///Reiniciar mensaje
            oSmtpClient.Dispose();
        }

        public ActionResult ModificarClientes(int idCliente)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaClienteID_Result modeloVista = new pa_RetornaClienteID_Result();
            modeloVista = this.modeloBD.pa_RetornaClienteID(idCliente).FirstOrDefault();

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarClientes(pa_RetornaClienteID_Result modeloVista)
        {

            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCedula(modeloVista.numCedula, modeloVista.idCliente.ToString()))
            {
                try
                {
                    DateTime fechaCliente = modeloVista.fechNacimiento;

                    cantRegistrosAfectados = this.modeloBD.pa_ModificaCliente(modeloVista.idCliente, modeloVista.nomCliente, modeloVista.ape1Cliente, modeloVista.ape2Cliente,
                                                                              modeloVista.numCedula, modeloVista.genero, modeloVista.provincia,
                                                                              modeloVista.fechNacimiento, modeloVista.canton, modeloVista.distrito,
                                                                              modeloVista.email, modeloVista.pTelefono, modeloVista.tipoCliente);
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
                mensaje = "Esta cédula ya existe, debes ingresar otra";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }


        public ActionResult ModificarClientesConsultor(int idCliente)
        {
            ///Obtener el registro que se desea modificar
            ///utilizando el parámetro del método idCliente
            pa_RetornaClienteID_Result modeloVista = new pa_RetornaClienteID_Result();
            modeloVista = this.modeloBD.pa_RetornaClienteID(idCliente).FirstOrDefault();

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult ModificarClientesConsultor(pa_RetornaClienteID_Result modeloVista)
        {

            ///Variable que registra la cantidad de registros afectados
            ///si un procedimiento que ejecuta insert, update o delete
            ///no afecta registros implica que hubo un error
            int cantRegistrosAfectados = 0;
            string mensaje = "";

            if (this.verificaCedula(modeloVista.numCedula, modeloVista.idCliente.ToString()))
            {
                try
                {
                    DateTime fechaCliente = modeloVista.fechNacimiento;

                    cantRegistrosAfectados = this.modeloBD.pa_ModificaCliente(modeloVista.idCliente, modeloVista.nomCliente, modeloVista.ape1Cliente, modeloVista.ape2Cliente,
                                                                              modeloVista.numCedula, modeloVista.genero, modeloVista.provincia,
                                                                              modeloVista.fechNacimiento, modeloVista.canton, modeloVista.distrito,
                                                                              modeloVista.email, modeloVista.pTelefono, modeloVista.tipoCliente);
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
                mensaje = "Esta cédula ya existe, debes ingresar otra";
            }

            Response.Write("<script language=javascript>alert('" + mensaje + "');</script>");

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        public ActionResult EliminarClientes(int idCliente)
        {
            ///Obtener el registro que se desea eliminar
            ///utilizando el parámetro del método idCliente
            pa_RetornaClienteID_Result modeloVista = new pa_RetornaClienteID_Result();
            ///Resultado de ejecución del procedimiento almacenado
            modeloVista = this.modeloBD.pa_RetornaClienteID(idCliente).FirstOrDefault();

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        [HttpPost]
        public ActionResult EliminarClientes(pa_RetornaClienteID_Result modeloVista)
        {
            /* Variable que registra la cantidad de registros afectados
             * si un procedeimiento que ejecuta insert, update o delete
             * si no afecta registros implica que hubo un error
             */
            int cantRegistrosAfectados = 0;
            string resultado = "";

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_EliminaCliente(modeloVista.idCliente);
            }
            catch (Exception error)
            {
                resultado = "Ocurrió un error" + error.Message;
            }
            finally
            {
                if (cantRegistrosAfectados > 0)
                {
                    resultado = "Registro eliminado";
                }
                else
                {
                    resultado += "No se pudo eliminar";
                }
            }

            this.AgregaGeneroViewBag();
            this.AgregaTipoClienteViewBag();
            ///Enviar el modelo a la vista
            return View(modeloVista);
        }

        string GetSHA256(string password)
        {
            SHA256 oSha256 = SHA256Managed.Create();
            ASCIIEncoding oEncoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder oStringBuilder = new StringBuilder();
            stream = oSha256.ComputeHash(oEncoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) oStringBuilder.AppendFormat("{0:x2}", stream[i]);

            return oStringBuilder.ToString();
        }

        [HttpPost]
        public ActionResult ModificaContrasena(int pIdCliente, string pContrasena)
        {
            string mensaje = "";
            int cantRegistrosAfectados = 0;

            string contrasenaEncriptada = this.GetSHA256(pContrasena);

            try
            {
                cantRegistrosAfectados = this.modeloBD.pa_ModificaPasswordCliente(pIdCliente, contrasenaEncriptada);
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
                    mensaje = "Contraseña actualizada";
                }
                else
                {
                    mensaje += ".No se pudo cambiar la contraseña";
                }
            }

            return Json(new { resultado = mensaje });
        }

        public ActionResult RpServiciosCliente()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult RetornaServiciosClienteLista()
        {
            List<pa_RetornaServiciosCliente_Result> serviciosCliente =
                this.modeloBD.pa_RetornaServiciosCliente("","","").ToList();

            return Json(new { resultado = serviciosCliente });
        }

        public ActionResult RpServiciosClienteConsultor()
        {
            return View();
        }

        public ActionResult RpServicioVehiculoConsultor()
        {
            return View();
        }

        public ActionResult RpVehiculosClienteConsultor()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RetornaServiciosClienteConsutorLista()
        {
            int dataUser = int.Parse(Session["idusuario"].ToString());

            List<pa_RetornaServiciosClienteConsultorID_Result> serviciosCliente =
                this.modeloBD.pa_RetornaServiciosClienteConsultorID(dataUser).ToList();

            return Json(new { resultado = serviciosCliente });
        }


        [HttpPost]
        public ActionResult RetornaVehiculosClienteConsutorLista()
        {
            int dataUser = int.Parse(Session["idusuario"].ToString());

            List<pa_RetornaServiciosClienteConsultorID_Result> serviciosCliente =
                this.modeloBD.pa_RetornaServiciosClienteConsultorID(dataUser).ToList();

            return Json(new { resultado = serviciosCliente });
        }


        [HttpPost]
        public ActionResult RetornaServiciosVehiculoConsutorLista()
        {
            int dataUser = int.Parse(Session["idusuario"].ToString());

            List<pa_RetornaServiciosClienteConsultorID_Result> serviciosCliente =
                this.modeloBD.pa_RetornaServiciosClienteConsultorID(dataUser).ToList();

            return Json(new { resultado = serviciosCliente });
        }
    }
}
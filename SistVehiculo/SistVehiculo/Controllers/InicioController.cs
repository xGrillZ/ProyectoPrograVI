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
    public class InicioController : Controller
    {
        /// <summary>
        /// Instancia del modelo de base de datos
        /// </summary>
        sistvehiculoviEntities modeloBD = new sistvehiculoviEntities();

        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }

       

        [HttpPost]
        public ActionResult Inicio(string correoElectronico, string password)
        {

            string mensaje = "";

           string encryptPass = this.GetSHA256(password);

            pa_RetornaClienteCorreoPwd_Result resultadoSp = this.modeloBD.pa_RetornaClienteCorreoPwd(correoElectronico, encryptPass).FirstOrDefault();

            try
            {
                if (resultadoSp == null)
                {
                    this.Session.Add("idusuario", null);
                    this.Session.Add("tipousuario", null);
                    this.Session.Add("usuariologueado", null);

                    mensaje = $"Correo electrónico o Contraseña inválida";
                    ///Mensaje de error si cumple lo contrario del verificado de datos nulos
                    Response.Write("<script>alert('" + mensaje + "')</script>");
                    return View();
                }
                else
                {
                    /*Session["User"] = resultadoSp;*/
                    this.Session.Add("idusuario", resultadoSp.idCliente);
                    this.Session.Add("tipousuario", resultadoSp.tipoCliente);
                    this.Session.Add("usuariologueado", true);

                    this.envioCorreoElectronico();
                    return RedirectToAction("Inicio", "Home");
                }
            }
            catch (Exception capturaExcepcion)
            {
                mensaje += $"Ocurrió un error: {capturaExcepcion}";
                ///Mensaje de error si cumple lo contrario del verificado de datos nulos
                Response.Write("<script>alert('" + mensaje + "')</script>");
                return View();
            }
        }

        void correoElectronicoIngreso(string pPriApellido, string pSecApellido, string pNombre, string pCorreo, DateTime pUltimoIngreso)
        {
            ///Creación de variables con datos
            string emailOrigen = "segurosxxiumca@gmail.com";
            string emailDestino = pCorreo;
            string contrasena = "CastroCarazoProgra";
            ///Creación del cuerpo del mensaje
            MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, "Nuevo inicio de sesión",
                                                       $"Bienvenido: {pPriApellido} {pSecApellido}" +
                                                       $" {pNombre}, usted ingresó por última vez: {pUltimoIngreso}");
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

        void envioCorreoElectronico()
        {
            ///Variable que almacena el IDCliente a la hora de iniciar sesión
            int dataUser = int.Parse(Session["idusuario"].ToString());

            pa_RetornaClienteID_Result retornaClienteID = new pa_RetornaClienteID_Result();

            retornaClienteID = modeloBD.pa_RetornaClienteID(dataUser).FirstOrDefault();

            this.correoElectronicoIngreso(retornaClienteID.ape1Cliente, retornaClienteID.ape2Cliente, 
                                          retornaClienteID.nomCliente, retornaClienteID.email, 
                                          retornaClienteID.ultimoIngreso);
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

        public ActionResult Principal()
        {
            return View();
        }

        public ActionResult ModificarClientesConsultor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModificarClientesConsultor(int idCliente)
        {

            string mensaje = "";

            pa_RetornaClienteID_Result resultadoSp = this.modeloBD.pa_RetornaClienteID(idCliente).FirstOrDefault();

            try
            {
                if (resultadoSp == null)
                {
                    this.Session.Add("idusuario", null);
                    this.Session.Add("tipousuario", null);
                    this.Session.Add("usuariologueado", null);

                    mensaje = $"Correo electrónico o Contraseña inválida";
                    ///Mensaje de error si cumple lo contrario del verificado de datos nulos
                    Response.Write("<script>alert('" + mensaje + "')</script>");
                    return View();
                }
                else
                {
                    /*Session["User"] = resultadoSp;*/
                    this.Session.Add("idusuario", resultadoSp.idCliente);
                    this.Session.Add("tipousuario", resultadoSp.tipoCliente);
                    this.Session.Add("usuariologueado", true);

                    this.envioCorreoElectronico();
                    return RedirectToAction("Modificar datos", "ModificarClientesConsultor");
                }
            }
            catch (Exception capturaExcepcion)
            {
                mensaje += $"Ocurrió un error: {capturaExcepcion}";
                ///Mensaje de error si cumple lo contrario del verificado de datos nulos
                Response.Write("<script>alert('" + mensaje + "')</script>");
                return View();
            }
        }
        public ActionResult RpServiciosClienteConsultor(string numCedula)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RetornaServiciosClienteConsutorLista(string numCedula)
        {
            List<pa_RetornaServiciosCliente_Result> serviciosCliente =
                this.modeloBD.pa_RetornaServiciosCliente("", "", "").ToList();

            return Json(new { resultado = serviciosCliente });
        }
    }
}

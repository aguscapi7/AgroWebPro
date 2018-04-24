using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Utilitarios
{
    public static class Utilitarios
    {
        public static void BitacoraErrores(string controlador, string accion, string mensaje)
        {

        }

        public static bool EnvioCorreo(string correoDestinatario, string asunto, string cuerpo, string correoSalida, string claveCorreoSalida)
        {
            bool enviado = false;
            try
            {
                //string correoSalida = ConfigurationManager.AppSettings["DireccionCorreo"].ToString();
                //string claveCorreoSalida = ConfigurationManager.AppSettings["ClaveCorreo"].ToString();

                if (!string.IsNullOrEmpty(correoSalida) && !string.IsNullOrEmpty(claveCorreoSalida))
                {
                    MailMessage email = new MailMessage();
                    email.To.Add(new MailAddress(correoDestinatario));
                    
                    email.From = new MailAddress(correoSalida);
                    email.Subject = asunto;
                    email.Body = cuerpo;
                    email.IsBodyHtml = true;
                    email.Priority = MailPriority.High;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(correoSalida, claveCorreoSalida);

                    smtp.Send(email);
                    email.Dispose();
                    enviado = true;
                }
            }
            catch (Exception ex)
            {

            }
            return enviado;
        }
        
        
        public static string Encriptar(string textoEncriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(textoEncriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        

        public static string DesEncriptar(string textoDesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(textoDesencriptar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}

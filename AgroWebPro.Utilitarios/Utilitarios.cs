using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgroWebPro.Utilitarios
{
    public static class Utilitarios
    {
        public static void BitacoraErrores(string excepcion, string origen, string nombreClase, string nombreMetodo)
        {
            try
            {
                
                //se establece el nombre del archivo de bitácora
                String nombreArchivoBitacora = DateTime.UtcNow.AddHours(-6).ToString("yyyyMMdd") + "_" + ConfigurationManager.AppSettings["NombreBitacora"].ToString() + ".txt";

                //se obtiene la ruta donde el archivo se creará
                String ruta = Path.Combine(ConfigurationManager.AppSettings["RutaBitacora"], nombreArchivoBitacora);

                //Verificamos que exista el directorio, en caso contrario lo creamos
                if (!Directory.Exists(ConfigurationManager.AppSettings["RutaBitacora"].ToString()))
                {
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["RutaBitacora"].ToString());
                }

                //se escribe en bitácora
                using (StreamWriter objWriter = File.AppendText(ruta))
                {
                    objWriter.WriteLine("**************************************************************************************************");
                    objWriter.WriteLine("Fecha: " + DateTime.UtcNow.AddHours(-6));
                    objWriter.WriteLine("Error: " + excepcion);
                    objWriter.WriteLine("Origen del error: " + origen);
                    objWriter.WriteLine("Nombre de la clase: " + nombreClase);
                    objWriter.WriteLine("Nombre del método: " + nombreMetodo);
                    objWriter.WriteLine("**************************************************************************************************");
                }
                
            }
            catch { }
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

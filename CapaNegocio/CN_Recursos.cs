
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// REFERENCIA CLAVE ENCRIPTADA
using System.Security.Cryptography;
// REFERENCIA ENVIO CORREOS
using System.Net.Mail;
using System.Net;
using System.IO;

namespace CapaNegocio
{
    public class CN_Recursos
    {

        // GENERAR CLAVE ALEATORIA
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }


        // ENCRIPTAR CLAVES DE ACCESO USUARIOS
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();

        }

        // ENVIAR CORREO A USUARIOS
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("@");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient() {
                    // CAMBIAR A SMTP DE ORIGEN - HOST Y PUERTO IGUALMENTE
                    Credentials = new NetworkCredential("@", ""),
                    Host = "",
                    Port = 0,
                    EnableSsl = true
                };
                smtp.Send(mail);
                resultado = true;

            }catch(Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }

        // CONVERTIR RUTA IMAGEN A BASE64
        public static string ConvertirBase64(string ruta, out bool conversion) {
            string textoBase64 = string.Empty;
            conversion = true;
            try
            {
                byte[] bytes = File.ReadAllBytes(ruta);
                textoBase64 = Convert.ToBase64String(bytes);
            }
            catch {
                conversion = false;
            }
            return textoBase64;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;

namespace KN_ProyectoWeb.Services
{
    public class Utilitarios
    {

        public string GenerarContrasenia()
        {
            int longitud = 8;
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] resultado = new char[longitud];

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[sizeof(uint)];

                for (int i = 0; i < longitud; i++)
                {
                    rng.GetBytes(buffer);
                    uint num = BitConverter.ToUInt32(buffer, 0);
                    resultado[i] = caracteres[(int)(num % (uint)caracteres.Length)];
                }
            }

            return new string(resultado);



        }
        public void EnviarCorreo(string asunto, string contenido, string destinatario)
        {
            var correoSMTP = ConfigurationManager.AppSettings["CorreoSMTP"];
            var contraseniaSMTP = ConfigurationManager.AppSettings["ContraseniaSMTP"];

            if (contraseniaSMTP != string.Empty)
            {

                var smtp = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(correoSMTP, contraseniaSMTP),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress("urboi123qtz@gmail.com"),
                    Subject = asunto,
                    Body = contenido,
                    IsBodyHtml = true
                };

                mensaje.To.Add(destinatario);

                smtp.Send(mensaje);
            }

        }
    }
}
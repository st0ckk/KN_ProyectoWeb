using KN_ProyectoWeb.EF;
using KN_ProyectoWeb.Models;
using KN_ProyectoWeb.Services;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        Utilitarios utilitarios = new Utilitarios();

        #region Inicio sesion
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            using (var context = new BD_KNEntities())
            {
                var resultado = context.tbUsuario
                    .Where(x => x.CorreoElectronico == usuario.CorreoElectronico && x.Contrasenia == usuario.Contrasenia && x.Estado == true).FirstOrDefault();

                //var resultado = context.ValidarUsuarios(usuario.CorreoElectronico, usuario.Contrasenia).FirstOrDefault();

                if (resultado != null)
                {
                    Session["NombreUsuario"] = resultado.Nombre;
                    Session["PerfilUsuario"] = resultado.tbPerfil.Nombre;
                    return RedirectToAction("Principal", "Home");
                }
                ViewBag.Mensaje = "La informacion es incorrecta.";
                return View();
            }
        }
        #endregion

        #region Registro
        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            using (var context = new BD_KNEntities())
            {
                //var nuevoUsuario = new tbUsuario
                //{
                //    Identificacion = usuario.Identificacion,
                //    Nombre = usuario.Nombre,
                //    CorreoElectronico = usuario.CorreoElectronico,
                //    Contrasenia = usuario.Contrasenia,
                //    ConsecutivoPerfil = 2, 
                //    Estado = true
                //};

                //context.tbUsuario.Add(nuevoUsuario);
                //context.SaveChanges();

                var resultado = context.CrearUsuarios(usuario.Identificacion, usuario.Nombre, usuario.CorreoElectronico, usuario.Contrasenia);
                if (resultado > 0)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Mensaje = "Error al registrar el usuario.";
                }
            }
            return View();
        }
        #endregion

        #region Recuperar acceso
        [HttpGet]
        public ActionResult RecuperarAcceso()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecuperarAcceso(Usuario usuario)
        {
            using (var context = new BD_KNEntities())
            {
                //Se valida si el usuario ya existe
                var resultadoConsulta = context.tbUsuario.Where(x => x.CorreoElectronico == usuario.CorreoElectronico).FirstOrDefault();

                //Si existe se manda a recupear el acceso
                if (resultadoConsulta != null)
                {
                    var NewContrasenia = utilitarios.GenerarContrasenia();

                    // actualizar la contraseña

                    resultadoConsulta.Contrasenia = NewContrasenia;

                    // nueva contraseña
                    var resultadoActualizacion = context.SaveChanges();

                    //envia correo
                    if (resultadoActualizacion > 0)
                    {
                        //StringBuilder mensaje = new StringBuilder();
                        //mensaje.AppendLine("Estimado(a) " + resultadoConsulta.Nombre + "<br>");
                        //mensaje.AppendLine("Se ha generado una consulta para reestablecer su contrasenia" + "<br><br>");
                        //mensaje.AppendLine("Su nueva contrasenia es: <b> " + NewContrasenia + "</b><br><br>");
                        //mensaje.AppendLine("Procure realizar el cambio de su contrasenia una vez ingrese al sistema.");
                        //mensaje.AppendLine("No responda a este mensaje, Muchas gracias");

                        string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
                        string path = Path.Combine(projectRoot, "TemplateRecuperacion.html");

                        // Leer todo el HTML
                        string htmlTemplate = System.IO.File.ReadAllText(path);

                        // Reemplazar placeholders
                        string mensaje = htmlTemplate
                            .Replace("{{Nombre}}", resultadoConsulta.Nombre)
                            .Replace("{{Contrasena}}", NewContrasenia);

                        //Enviar correo

                        utilitarios.EnviarCorreo("Recuperación de contraseña", NewContrasenia, resultadoConsulta.CorreoElectronico);
                        return RedirectToAction("index", "Home");
                    }

                }

                ViewBag.Mensaje = "La información no se ha podido reestablecer";
                return View();
            }
        }
        #endregion

        public ActionResult Principal()
        { 
            
            return View(); 
        
        }

        #region Cerrar Sesion
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}


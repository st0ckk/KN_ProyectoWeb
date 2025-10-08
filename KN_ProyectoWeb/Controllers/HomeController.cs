using KN_ProyectoWeb.EF;
using KN_ProyectoWeb.Models;
using System.Linq;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {

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
                //var resultado = context.tbUsuario
                //    .Where(x => x.CorreoElectronico == usuario.CorreoElectronico && x.Contrasenia == usuario.Contrasenia && x.Estado == true).FirstOrDefault();

                var resultado = context.ValidarUsuarios(usuario.CorreoElectronico, usuario.Contrasenia).FirstOrDefault();

                if (resultado != null)
                {
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
    }
}

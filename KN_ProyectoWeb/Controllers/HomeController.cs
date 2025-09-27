using KN_ProyectoWeb.Models;
using System.Web.Mvc;

namespace KN_ProyectoWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            return RedirectToAction("Principal","Home");
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(Usuario usuario)
        {
            return View();
        }
        [HttpGet]
        public ActionResult RecuperarAcceso()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecuperarAcceso(Usuario usuario)
        {
            return View();
            /* validar que el usuario exista
             generar clave temp
            enviar clave temp*/
        }
        public ActionResult Principal()
        {
            return View();
        }
    }
}

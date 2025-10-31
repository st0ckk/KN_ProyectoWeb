using KN_ProyectoWeb.EF;
using KN_ProyectoWeb.Models;
using KN_ProyectoWeb.Services;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;
using System.Web.UI;

namespace KN_ProyectoWeb.Controllers
{
    
    [Seguridad]

    public class ProductosController : Controller
    {
        [HttpGet]
        public ActionResult VerProductos()
        {
            using (var context = new BD_KNEntities())
            {
                //Tomar el objeto de la BD
                var resultado = context.tbProducto.Include("tbCategoria").ToList();

                var datos = resultado.Select(p => new Producto
                {
                    ConsecutivoProducto = p.ConsecutivoProducto,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    NombreCategoria = p.tbCategoria.Nombre,
                    Estado = p.Estado,
                    Imagen = p.Imagen
                }).ToList();

                return View(datos);
            }
        }
    }
}
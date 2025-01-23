using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionTienda.Controllers
{
    public class MarcaController : Controller
    {
        private CN_Marca objNegocio = new CN_Marca();

        [HttpGet]
        public ActionResult ListaMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();
            return View(oLista);
        }

        public ActionResult Registrar()
        {
            return View();
        }

        // REGISTRAR NUEVA 
        [HttpPost]
        public ActionResult Registrar(Marca objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                ViewBag.Error = "La descripción de la marca no puede estar vacía.";
                return View(objeto);
            }

            resultado = objNegocio.Registrar(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("ListaMarcas", "Marca");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View(objeto);
            }
        }

        // VISTA PARA EDITAR 
        public ActionResult Editar(int id)
        {
            Marca marca = objNegocio.Listar().FirstOrDefault(c => c.IdMarca == id);
            if (marca == null)
            {
                return RedirectToAction("ListaMarcas", "Marca");
            }
            return View(marca);
        }

        // EDITAR 
        [HttpPost]
        public ActionResult Editar(Marca objeto)
        {
            string mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                ViewBag.Error = "La descripción de la marca no puede estar vacía.";
                return View(objeto);
            }

            bool resultado = objNegocio.Editar(objeto, out mensaje);

            if (resultado)
            {
                ViewBag.Error = null;
                return RedirectToAction("ListaMarcas", "Marca");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View(objeto);
            }
        }

        public ActionResult Eliminar(int id)
        {
            string mensaje = string.Empty;
            bool resultado = objNegocio.Eliminar(id, out mensaje);

            if (resultado)
            {
                ViewBag.Success = "Marca eliminada exitosamente.";
                return RedirectToAction("ListaMarcas", "Marca");
            }
            else
            {
                ViewBag.Error = mensaje;
                return RedirectToAction("ListaMarcas", "Marca");
            }
        }

    }
}

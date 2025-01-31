using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdministrador.Controllers
{
    public class CategoriaController : Controller
    {
        private CN_Categoria objNegocio = new CN_Categoria();

        [HttpGet]
        public ActionResult ListaCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return View(oLista);
        }

        public ActionResult Registrar()
        {
            return View();
        }

        // REGISTRAR NUEVA 
        [HttpPost]
        public ActionResult Registrar(Categoria objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                ViewBag.Error = "La descripción de la categoría no puede estar vacía.";
                return View(objeto);
            }

            resultado = objNegocio.Registrar(objeto, out mensaje);

            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("ListaCategorias", "Categoria");
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
            Categoria categoria = objNegocio.Listar().FirstOrDefault(c => c.IdCategoria == id);
            if (categoria == null)
            {
                return RedirectToAction("ListaCategorias", "Categoria");
            }
            return View(categoria);
        }

        // EDITAR 
        [HttpPost]
        public ActionResult Editar(Categoria objeto)
        {
            string mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                ViewBag.Error = "La descripción de la categoría no puede estar vacía.";
                return View(objeto);
            }

            bool resultado = objNegocio.Editar(objeto, out mensaje);

            if (resultado)
            {
                ViewBag.Error = null;
                return RedirectToAction("ListaCategorias", "Categoria");
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
                ViewBag.Success = "Categoría eliminada exitosamente.";
                return RedirectToAction("ListaCategorias", "Categoria");
            }
            else
            {
                ViewBag.Error = mensaje;
                return RedirectToAction("ListaCategorias", "Categoria");
            }
        }
    }
}
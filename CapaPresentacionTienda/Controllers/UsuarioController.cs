using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionTienda.Controllers
{
    public class UsuarioController : Controller
    {
        // LISTAR TODAS LAS Usuarios
        [HttpGet]
        public ActionResult ListaUsuarios()
        {
            List<Administrativo> oLista = new List<Administrativo>();
            oLista = new CN_Administrativo().Listar();
            return View(oLista);
        }
        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        // REGISTRAR NUEVOS Admin
        public ActionResult Registrar(Administrativo objeto)
        {
            int resultado;
            string mensaje = string.Empty;
            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.Nombres) ? "" : objeto.Nombres;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.Apellidos) ? "" : objeto.Apellidos;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.Correo) ? "" : objeto.Correo;
            ViewData["Clave"] = string.IsNullOrEmpty(objeto.Clave) ? "" : objeto.Clave;
            resultado = new CN_Administrativo().Registrar(objeto, out mensaje);
            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("ListaUsuarios", "Usuario");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }
    }
}
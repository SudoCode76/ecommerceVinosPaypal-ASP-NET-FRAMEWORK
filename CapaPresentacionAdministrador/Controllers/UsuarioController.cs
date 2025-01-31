using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdministrador.Controllers
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
            CN_Administrativo adminNegocio = new CN_Administrativo();

            // Verificar si el correo ya está registrado
            if (adminNegocio.ExisteCorreo(objeto.Correo))
            {
                TempData["existe"] = "El correo electrónico ya está registrado en el sistema.";
                return View();
            }
            // Validar los campos obligatorios
            if (string.IsNullOrEmpty(objeto.Nombres) || string.IsNullOrEmpty(objeto.Apellidos) ||
                string.IsNullOrEmpty(objeto.Correo) || string.IsNullOrEmpty(objeto.Clave))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            // Validar formato del correo
            if (!EsCorreoValido(objeto.Correo))
            {
                ViewBag.Error = "El formato del correo electrónico es incorrecto.";
                return View();
            }

            // Almacenar los datos en ViewData para repoblar el formulario en caso de error
            ViewData["Nombres"] = objeto.Nombres;
            ViewData["Apellidos"] = objeto.Apellidos;
            ViewData["Correo"] = objeto.Correo;
            ViewData["Clave"] = objeto.Clave;

            // Intentar registrar el usuario
            resultado = new CN_Administrativo().Registrar(objeto, out mensaje);
            if (resultado > 0)
            {
                ViewBag.Error = null;
                TempData["Success"] = "Registro exitoso. El usuario ha sido registrado correctamente.";
                return RedirectToAction("ListaUsuarios", "Usuario");
            }
            else
            {
                TempData["Error"] = mensaje;
                return View();
            }
        }



        private bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrEmpty(correo))
                return false;

            var regexCorreo = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regexCorreo.IsMatch(correo);
        }


        public ActionResult Eliminar(int id)
        {
            string mensaje = string.Empty;
            bool resultado = new CN_Administrativo().Eliminar(id, out mensaje);
            if (resultado)
            {
                ViewBag.Error = null;
                TempData["Success"] = "El usuario ha sido eliminado exitosamente.";
                return RedirectToAction("ListaUsuarios", "Usuario");

            }
            else
            {
                TempData["Error"] = mensaje;
                return RedirectToAction("ListaUsuarios", "Usuario");
            }
        }
    }
}
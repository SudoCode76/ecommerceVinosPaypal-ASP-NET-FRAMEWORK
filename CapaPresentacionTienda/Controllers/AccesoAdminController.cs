
using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoAdminController : Controller
    {
        
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        // INICIO DE SESION Admin
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Administrativo oCliente = null;
            oCliente = new CN_Administrativo().Listar().Where(cliente => cliente.Correo == correo && cliente.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();
            if (oCliente == null)
            {
                ViewBag.Error = "Lo sentimos, el correo y/o contraseņa no son correctas";
                return View();
            }
            else
            {
                    FormsAuthentication.SetAuthCookie(oCliente.Correo, false);
                    Session["Admin"] = oCliente;
                    ViewBag.Error = null;
                    return RedirectToAction("ListaUsuarios", "Usuario");
            }
        }


        public ActionResult CerrarSesion()
        {
            Session["Admin"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "AccesoAdmin");
        }


    }
}
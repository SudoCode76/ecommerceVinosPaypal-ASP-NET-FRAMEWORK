using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdministrador.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult vistaDashboard()
        {
            DashBoard objeto = new CN_Reporte().VerDashboard();
            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet); 
        }

    }
}
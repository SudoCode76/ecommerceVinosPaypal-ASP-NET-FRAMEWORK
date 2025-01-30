using CapaNegocio;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdministrador.Controllers
{
    public class VentasController : Controller
    {
        private CN_Venta objNegocio = new CN_Venta();
        [HttpGet]
        public ActionResult ListaVentas()
        {
            List<Venta> oLista = new List<Venta>();
            oLista = new CN_Venta().Listar();
            return View(oLista);
        }

        public ActionResult Eliminar(int id)
        {
            string mensaje = string.Empty;
            bool resultado = objNegocio.Eliminar(id, out mensaje);
            if (resultado)
            {
                return RedirectToAction("ListaVentas", "Ventas");
            }
            else
            {
                ViewBag.Error = mensaje;
                return RedirectToAction("ListaVentas", "Ventas");
            }
        }

        public ActionResult DetalleVenta(int id)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();
            oLista = new CN_Venta().ListarCompras(id);
            return View(oLista);
        }
    }
}
using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;  // Agregar esta línea


namespace CapaPresentacionTienda.Controllers
{
    public class ProductoController : Controller
    {
        private CN_Producto capaNegocio = new CN_Producto();

        public ActionResult ListaProductos()
        {
            List<Producto> lista = new List<Producto>();
            lista = new CN_Producto().Listar();
            return View(lista);
        }

        public ActionResult Registrar()
        {
            var categorias = new CN_Categoria().Listar(); // Esto debe devolver una lista de categorías.
            var marcas = new CN_Marca().Listar(); // Esto debe devolver una lista de marcas.

            // Pasar las categorías y marcas a la vista
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Descripcion");
            ViewBag.Marcas = new SelectList(marcas, "IdMarca", "Descripcion");

            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Producto objProducto, HttpPostedFileBase Imagen)
        {
            // Verificar si la imagen fue cargada
            if (Imagen != null && Imagen.ContentLength > 0)
            {
                // Ruta base para guardar la imagen
                string rutaBase = Server.MapPath("~/images/productos/");

                // Verificar si la carpeta existe, si no, crearla
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                // Generar un nombre único para la imagen
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);  // Crear la ruta completa usando Path.Combine

                try
                {
                    // Guardar la imagen en el servidor
                    Imagen.SaveAs(rutaCompleta);

                    // Guardar la ruta relativa de la imagen en el objeto Producto
                    objProducto.RutaImagen = "~/images/productos/" + nombreArchivo;
                    objProducto.NombreImagen = nombreArchivo;
                }
                catch (Exception ex)
                {
                    // En caso de error, devolver el mensaje de error
                    ViewBag.Error = "Error al guardar la imagen: " + ex.Message;
                    ViewBag.Categorias = new SelectList(new CN_Categoria().Listar(), "IdCategoria", "Descripcion");
                    ViewBag.Marcas = new SelectList(new CN_Marca().Listar(), "IdMarca", "Descripcion");
                    return View(objProducto);
                }
            }

            // Registrar el producto en la base de datos
            string mensaje;
            int resultado = capaNegocio.Registrar(objProducto, out mensaje);

            if (resultado > 0)
            {
                // Si se registró el producto correctamente, redirigir a la lista de productos
                return RedirectToAction("ListaProductos", "Producto");
            }
            else
            {
                // Si hubo un error, mostrar el mensaje
                ViewBag.Error = mensaje;
                ViewBag.Categorias = new SelectList(new CN_Categoria().Listar(), "IdCategoria", "Descripcion");
                ViewBag.Marcas = new SelectList(new CN_Marca().Listar(), "IdMarca", "Descripcion");
                return View(objProducto);
            }
        }


        public ActionResult Editar(int id)
        {
            // Obtener el producto con el id proporcionado
            Producto producto = capaNegocio.Listar().FirstOrDefault(c => c.IdProducto == id);
            if (producto == null)
            {
                return HttpNotFound(); // Esto indica que el producto no se encuentra en la base de datos
            }

            // Obtener las categorías y marcas para llenar los dropdowns
            var categorias = new CN_Categoria().Listar();
            var marcas = new CN_Marca().Listar();

            // Pasar las categorías y marcas al ViewBag
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Descripcion");
            ViewBag.Marcas = new SelectList(marcas, "IdMarca", "Descripcion");

            return View(producto);
        }


        [HttpPost]
        public ActionResult Editar(Producto objProducto, HttpPostedFileBase Imagen)
        {
            // Verificar si la imagen fue cargada
            if (Imagen != null && Imagen.ContentLength > 0)
            {
                string rutaBase = Server.MapPath("~/images/productos/");
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);

                try
                {
                    Imagen.SaveAs(rutaCompleta);
                    objProducto.RutaImagen = "~/images/productos/" + nombreArchivo;
                    objProducto.NombreImagen = nombreArchivo; // Establecer el nombre de la imagen
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error al guardar la imagen: " + ex.Message;
                    ViewBag.Categorias = new SelectList(new CN_Categoria().Listar(), "IdCategoria", "Descripcion");
                    ViewBag.Marcas = new SelectList(new CN_Marca().Listar(), "IdMarca", "Descripcion");
                    return View(objProducto);
                }
            }

            // Verificar si se realizaron cambios en el producto
            bool resultado = capaNegocio.Editar(objProducto, out string mensaje);

            if (resultado)
            {
                ViewBag.Error = null;
                return RedirectToAction("ListaProductos", "Producto");
            }
            else
            {
                ViewBag.Error = mensaje;
                ViewBag.Categorias = new SelectList(new CN_Categoria().Listar(), "IdCategoria", "Descripcion");
                ViewBag.Marcas = new SelectList(new CN_Marca().Listar(), "IdMarca", "Descripcion");
                return View(objProducto);
            }
        }



        public ActionResult Eliminar(int id)
        {
            string mensaje;
            bool resultado = capaNegocio.Eliminar(id, out mensaje);

            if (resultado)
            {
                ViewBag.Success = "Producto registrado correctamente.";
                return RedirectToAction("ListaProductos", "Producto");

            }
            else
            {
                ViewBag.Error = mensaje;
                return RedirectToAction("ListaProductos", "Producto");
            }

        }

        [HttpPost]
        public ActionResult GuardarImagen(int idProducto, HttpPostedFileBase imagen)
        {
            if (imagen != null && imagen.ContentLength > 0)
            {
                string ruta = Server.MapPath("~/imagenes/productos/");
                string nombreArchivo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(imagen.FileName);
                string rutaCompleta = ruta + nombreArchivo;

                try
                {
                    imagen.SaveAs(rutaCompleta);

                    var producto = new Producto
                    {
                        IdProducto = idProducto,
                        RutaImagen = "~/imagenes/productos/" + nombreArchivo
                    };

                    string mensaje;
                    bool resultado = capaNegocio.GuardarDatosImagen(producto, out mensaje);

                    if (resultado)
                    {
                        
                        ViewBag.Success = "imagen Guardada correctamente.";
                    }
                    else
                    {
                        ViewBag.Error = mensaje;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error al guardar la imagen: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Error = "No se seleccionó ninguna imagen.";
            }

            return RedirectToAction("ListaProductos", "Producto");
        }
    }
}

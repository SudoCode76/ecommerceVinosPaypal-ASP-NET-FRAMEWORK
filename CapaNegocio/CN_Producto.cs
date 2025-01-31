
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Producto
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE PRODUCTOS
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        // REGISTRO DE NUEVAS ProductoS
        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del Producto no puede estar vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del Producto no puede estar vacia";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debes seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debes seleccionar una categoria";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "El precio del producto no puede estar vacio";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "El stock del producto no puede estar vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            } // CIERRE if (string.IsNullOrEmpty(Mensaje))
            else
            {
                return 0;
            }
        }

        // EDITAR ProductoS
        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones de los campos
            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del Producto no puede estar vacio";
                return false;  // Retorna false si la validación falla
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del Producto no puede estar vacia";
                return false;  // Retorna false si la validación falla
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debes seleccionar una marca";
                return false;  // Retorna false si la validación falla
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debes seleccionar una categoria";
                return false;  // Retorna false si la validación falla
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "El precio del producto no puede estar vacio";
                return false;  // Retorna false si la validación falla
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "El stock del producto no puede estar vacio";
                return false;  // Retorna false si la validación falla
            }

            // Llamada a la capa de datos si las validaciones son correctas
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        // ELIMINAR ProductoS
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        // REGISTRO RUTA DE IMAGENES PRODUCTOS
        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }
    }
}

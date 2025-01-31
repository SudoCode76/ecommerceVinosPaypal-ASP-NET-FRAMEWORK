
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        // ACCEDIENDO A TODOS LOS DATOS DE LA CAPA DATOS DE CARRITO DE COMPRAS
        private CD_Carrito objCapaDato = new CD_Carrito();
        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            return objCapaDato.ExisteCarrito(idcliente, idproducto);
        }

        // GESTION DE TODOS LOS ARTICULOS AGREGADOS POR CLIENTES -> CARRITO DE COMPRAS
        public bool OperacionCarrito(int idcliente, int idproducto, bool sumar, out string Mensaje)
        {
            return objCapaDato.OperacionCarrito(idcliente, idproducto, sumar, out Mensaje);
        }
        // TOTAL DE PRODUCTOS AGREGADOS EN CARRITO DE COMPRAS
        public int CantidadEnCarrito(int idcliente)
        {
            return objCapaDato.CantidadEnCarrito(idcliente);
        }
        // OBTENER EL LISTADO DE PRODUCTOS CARRITO DE COMPRAS
        public List<Carrito> ListarProducto(int idcliente)
        {
            return objCapaDato.ListarProducto(idcliente);
        }
        // ELIMINAR PRODUCTOS REGISTRADOS EN CARRITO DE COMPRAS
        public bool EliminarCarrito(int idcliente, int idproducto)
        {
            return objCapaDato.EliminarCarrito(idcliente, idproducto);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        // REFERENCIA AL OBJETO ID CLIENTE DE LA CLASE CLIENTE
        public Cliente oCliente { get; set; }
        // REFERENCIA AL OBJETO ID PRODUCTO DE LA CLASE PRODUCTO
        public Producto oProducto { get; set; }
        public int Cantidad { get; set; }
    }
}

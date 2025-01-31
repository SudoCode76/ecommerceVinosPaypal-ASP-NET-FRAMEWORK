
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        // CONEXION A BASE DE DATOS DESDE WEBCONFIG
        public static string cn = ConfigurationManager.ConnectionStrings["cadena"].ToString();
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapaDato = new CD_Ubicacion();
        // RETORNAR LISTADO DE DEPARTAMENTOS
        public List<Departamento> ObtenerDepartamento()
        {
            return objCapaDato.ObtenerDepartamento();
        }
        // RETORNAR LISTADO DE MUNICIPIOS
        public List<Municipio> ObtenerMunicipio(string iddepartamento)
        {
            return objCapaDato.ObtenerMunicipio(iddepartamento);
        }
    }
}

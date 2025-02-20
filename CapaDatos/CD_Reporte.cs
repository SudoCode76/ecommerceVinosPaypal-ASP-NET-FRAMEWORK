﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public DashBoard VerDashboard()
        {
            DashBoard objeto = new DashBoard();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    // REALIZA LECTURA DE DATOS
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            objeto = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"])
                            };

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                objeto = new DashBoard();
            }
            return objeto;
        }
    }
}

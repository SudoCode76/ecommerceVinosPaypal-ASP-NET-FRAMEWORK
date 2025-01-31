
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CapaEntidad;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    namespace CapaDatos
    {
        public class CD_Venta
        {

            public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
            {
                bool respuesta = false;
                Mensaje = string.Empty;
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                    {
                        SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oconexion);
                        cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                        cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
                        cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                        cmd.Parameters.AddWithValue("Contacto", obj.Contacto);
                        cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                        cmd.Parameters.AddWithValue("IdTransaccion", obj.IdTransaccion);
                        cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                        cmd.Parameters.AddWithValue("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        //cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                        cmd.Parameters.AddWithValue("Mensaje", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        oconexion.Open();

                        cmd.ExecuteNonQuery();

                        respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    respuesta = false;
                    Mensaje = ex.Message;
                }
                return respuesta;
            }

            // LISTAR TODAS LAS COMPRAR REALIZADAS POR CLIENTES
            public List<DetalleVenta> ListarCompras(int idcliente)
            {
                List<DetalleVenta> lista = new List<DetalleVenta>();
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                    {
                        string query = "select * from fn_ListarCompra(@idcliente)";
                        SqlCommand cmd = new SqlCommand(query, oconexion);
                        cmd.Parameters.AddWithValue("@idcliente", idcliente);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();
                        // REALIZA LECTURA DE DATOS
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // LLENADO DE LISTA SEGUN DATOS COINCIDENTES
                                lista.Add(new DetalleVenta()
                                {
                                    oProducto = new Producto()
                                    {
                                        Nombre = dr["Nombre"].ToString(),
                                        Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-SV")),
                                        RutaImagen = dr["RutaImagen"].ToString(),
                                        NombreImagen = dr["NombreImagen"].ToString(),
                                    },
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-SV")),
                                    IdTransaccion = dr["IdTransaccion"].ToString()
                                }
                                );

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    lista = new List<DetalleVenta>();
                }
                return lista;
            }




            //ver todas las ventas
            public List<Venta> Listar()
            {
                List<Venta> lista = new List<Venta>();
                try
                {
                    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                    {
                        string query = @"
                    SELECT v.IdVenta,  c.Nombres + ' ' + c.Apellidos AS NombreCliente, v.TotalProducto, v.MontoTotal, 
                           v.Contacto, v.Telefono, v.Direccion, v.IdTransaccion
                    FROM Venta v
                    INNER JOIN Cliente c ON v.IdCliente = c.IdCliente"; 

                        SqlCommand cmd = new SqlCommand(query, oconexion);
                        cmd.CommandType = CommandType.Text;
                        oconexion.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Venta()
                                {
                                    IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                    NombreCliente = dr["NombreCliente"].ToString(), // Agregado el nombre del cliente
                                    TotalProducto = Convert.ToInt32(dr["TotalProducto"]),   
                                    MontoTotal = Convert.ToDecimal(dr["MontoTotal"], new CultureInfo("es-SV")),
                                    Contacto = dr["Contacto"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    IdTransaccion = dr["IdTransaccion"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    lista = new List<Venta>();
                }
                return lista;
            }



        }
    }


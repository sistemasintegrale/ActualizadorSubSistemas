using Common;
using Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class GeneralData
    {

        public List<Usuario> listarUsuarios()
        {
            List<Usuario> list = new();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conneccion.GetConexion()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SGES_USUARIO_LISTAR", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = int.MaxValue;
                        cmd.Parameters.AddWithValue("@usua_secured_key", "ACCESSKEY");
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            list.Add(new Usuario
                            {
                                usua_icod_usuario = Convert.ToInt32(reader["usua_icod_usuario"]),
                                usua_codigo_usuario = reader["usua_codigo_usuario"].ToString()!.Trim(),
                                usua_nombre_usuario = reader["usua_nombre_usuario"].ToString()!.Trim(),
                                usua_password_usuario = reader["usua_password_usuario"].ToString()!,
                                usua_iactivo = Convert.ToBoolean(reader["usua_iactivo"]),
                                strEstado = Convert.ToBoolean(reader["usua_iactivo"]) ? "Activo" : "Inactivo",
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return list;
        }
        public void EquipoModificar(Equipo equipo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_MODIFICAR", con);
                    cm.Parameters.AddWithValue("@Id", equipo.Id);
                    cm.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                    cm.Parameters.AddWithValue("@Cpu", equipo.Cpu);
                    cm.Parameters.AddWithValue("@UbicacionActualizador", equipo.UbicacionActualizador);
                    cm.Parameters.AddWithValue("@NombreUsuario", equipo.NombreUsuario);
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int EquipoIngresar(Equipo equipo)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_INSERTAR", con);
                    cm.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                    cm.Parameters.AddWithValue("@Cpu", equipo.Cpu);
                    cm.Parameters.AddWithValue("@UbicacionActualizador", equipo.UbicacionActualizador);
                    cm.Parameters.AddWithValue("@NombreUsuario", equipo.NombreUsuario);
                    cm.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();
                    equipo.Id = Convert.ToInt32(cm.Parameters["@Id"].Value);

                }
            }
            catch (Exception)
            {

                throw;
            }
            return equipo.Id;
        }

        public List<Equipo> EquipoListar()
        {
            List<Equipo> lista = new List<Equipo>();
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_LISTAR", con);
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Equipo()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Cpu = reader["Cpu"].ToString(),
                            UbicacionActualizador = reader["UbicacionActualizador"].ToString(),
                            NombreUsuario = reader["NombreUsuario"].ToString()
                        });
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        public int SistemaIngresar(Sistema sistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("SISTEMA_INSERTAR", con);
                    cm.Parameters.AddWithValue("@Nombre", sistema.Nombre);
                    cm.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();
                    sistema.Id = Convert.ToInt32(cm.Parameters["@Id"].Value);

                }
            }
            catch (Exception)
            {

                throw;
            }
            return sistema.Id;
        }

        public void SistemaModificar(Sistema sistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand("SISTEMA_MODIFICAR", con);
                    cm.Parameters.AddWithValue("@Id", sistema.Id);
                    cm.Parameters.AddWithValue("@Nombre", sistema.Nombre);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Sistema> SistemaListar()
        {
            List<Sistema> lista = new List<Sistema>();
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("SISTEMA_LISTAR", con);
                    con.Open();
                    SqlDataReader reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Sistema()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                        });
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public int SubSistemaIngresar(SubSistema subSistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("SISTEMA_SUB_INSERTAR", con);
                    cm.Parameters.AddWithValue("@IdSistema", subSistema.IdSistema);
                    cm.Parameters.AddWithValue("@Link", subSistema.Link);
                    cm.Parameters.AddWithValue("@Fecha", subSistema.Fecha);
                    cm.Parameters.AddWithValue("@Disponible", subSistema.Disponible);
                    cm.Parameters.AddWithValue("@Comentarios", subSistema.Comentarios);
                    cm.Parameters.AddWithValue("@Nombre", subSistema.Nombre);
                    cm.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();
                    subSistema.Id = Convert.ToInt32(cm.Parameters["@Id"].Value);

                }
            }
            catch (Exception)
            {

                throw;
            }
            return subSistema.Id;
        }

        public void SubSistemaModificar(SubSistema subSistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("SISTEMA_SUB_MODIFICAR", con);
                    cm.Parameters.AddWithValue("@Id", subSistema.Id);
                    cm.Parameters.AddWithValue("@IdSistema", subSistema.IdSistema);
                    cm.Parameters.AddWithValue("@Link", subSistema.Link);
                    cm.Parameters.AddWithValue("@Fecha", subSistema.Fecha);
                    cm.Parameters.AddWithValue("@Disponible", subSistema.Disponible);
                    cm.Parameters.AddWithValue("@Comentarios", subSistema.Comentarios);
                    cm.Parameters.AddWithValue("@Nombre", subSistema.Nombre);
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SubSistema> SubSistemaListar()
        {
            List<SubSistema> lista = new List<SubSistema>();
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("SISTEMA_SUB_LISTAR", con);
                    con.Open();
                    SqlDataReader reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new SubSistema()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Link = reader["Link"].ToString(),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Disponible = Convert.ToBoolean(reader["Disponible"]),
                            Comentarios = reader["Comentarios"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            IdSistema = Convert.ToInt32(reader["IdSistema"])
                        });
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public void EquipoSubSistemaIngresar(EquiposSubSistema equiposSubSistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_SUB_SISTEMA_INSERTAR", con);
                    cm.Parameters.AddWithValue("@IdEquipo", equiposSubSistema.IdEquipo);
                    cm.Parameters.AddWithValue("@IdSubSistema", equiposSubSistema.IdSubSistema);
                    cm.Parameters.AddWithValue("@FechaActualizacion", equiposSubSistema.FechaActualizacion);
                    cm.Parameters.AddWithValue("@Acceso", equiposSubSistema.Acceso);
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EquipoSubSistemaModificar(EquiposSubSistema equiposSubSistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_SUB_SISTEMA_MODIFICAR", con);
                    cm.Parameters.AddWithValue("@IdEquipo", equiposSubSistema.IdEquipo);
                    cm.Parameters.AddWithValue("@IdSubSistema", equiposSubSistema.IdSubSistema);
                    cm.Parameters.AddWithValue("@Acceso", equiposSubSistema.Acceso);
                    cm.Parameters.AddWithValue("@FechaActualizacion", equiposSubSistema.FechaActualizacion);
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EquipoSubSistemaModificarDarAcceso(EquiposSubSistema equiposSubSistema)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_SUB_SISTEMA_MODIFICAR_ACCESO", con);
                    cm.Parameters.AddWithValue("@IdEquipo", equiposSubSistema.IdEquipo);
                    cm.Parameters.AddWithValue("@IdSistema", equiposSubSistema.IdSistema);
                    cm.Parameters.AddWithValue("@Acceso", equiposSubSistema.Acceso);
                    cm.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cm.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EquiposSubSistema> EquipoSubSistemaListar()
        {
            List<EquiposSubSistema> lista = new List<EquiposSubSistema>();
            try
            {
                using (SqlConnection con = new SqlConnection(Conneccion.GetConexion()))
                {
                    SqlCommand cm = new SqlCommand("EQUIPO_SUB_SISTEMA_LISTAR", con);
                    con.Open();
                    SqlDataReader reader = cm.ExecuteReader();
                    while (reader.Read())
                    {
                        var data = new EquiposSubSistema();

                        data.IdEquipo = Convert.ToInt32(reader["IdEquipo"]);
                        var dt = reader["IdSubSistema"].ToString();
                        data.IdSubSistema = string.IsNullOrEmpty(dt) ? 0 : Convert.ToInt32(dt);
                        data.FechaActualizacion = Convert.ToDateTime(reader["FechaActualizacion"]);
                        data.Acceso = Convert.ToBoolean(reader["Acceso"]);
                        data.IdSistema = Convert.ToInt32(reader["IdSistema"]);
                        lista.Add(data);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
    }
}

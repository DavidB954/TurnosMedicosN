using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Especialidad
    {
        DAL_Conexion conex = new DAL_Conexion();

        public void CrearEspecialidad(BE_Especialidad especialidad)
        {
            try
            {
                 using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                SqlCommand cmdEspecialidad = new SqlCommand(@"Insert into Especialidades (Nombre) VALUES (@nombre)", conexion);

                cmdEspecialidad.Parameters.Add("@nombre", SqlDbType.NVarChar, 30).Value = especialidad.Nombre;

                cmdEspecialidad.ExecuteNonQuery();  
            }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminarEspecialidad(int id)
        {
            try
            {

                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    SqlCommand cmdEspecidadBorrar = new SqlCommand(@"Delete from Especialidades where IdEspecialidad = @id", conexion);
                    cmdEspecidadBorrar.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmdEspecidadBorrar.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizarValorTurno(int idEspecialidad, decimal valorTurno, decimal porcentajeAusente)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        UPDATE Especialidades SET ValorTurno = @valorTurno, PorcentajeAusente = @porcentaje
                        WHERE IdEspecialidad = @idEspecialidad", conexion);
                    SqlParameter valor = cmd.Parameters.Add("@valorTurno", SqlDbType.Decimal);
                    valor.Precision = 12;
                    valor.Scale = 2;
                    valor.Value = valorTurno;
                    SqlParameter porcentaje = cmd.Parameters.Add("@porcentaje", SqlDbType.Decimal);
                    porcentaje.Precision = 5;
                    porcentaje.Scale = 2;
                    porcentaje.Value = porcentajeAusente;
                    cmd.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = idEspecialidad;
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new InvalidOperationException("La especialidad no existe.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_Especialidad> ListarEspecialidades()
        {
            try
            {

                List<BE_Especialidad> _listaEspecialidad = new List<BE_Especialidad>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmdListaEspecialidad = new SqlCommand(
                        "Select IdEspecialidad, Nombre, ValorTurno, PorcentajeAusente from Especialidades Order by Nombre", conexion);

                    SqlDataReader lector = cmdListaEspecialidad.ExecuteReader();

                    while (lector.Read())
                    {
                        BE_Especialidad especialidad = new BE_Especialidad();
                        especialidad.IdEspecialidad = Convert.ToInt32(lector["IdEspecialidad"]);
                        especialidad.Nombre = lector["Nombre"].ToString();
                        especialidad.ValorTurno = Convert.ToDecimal(lector["ValorTurno"]);
                        especialidad.PorcentajeAusente = Convert.ToDecimal(lector["PorcentajeAusente"]);

                        _listaEspecialidad.Add(especialidad);
                    }

                    return _listaEspecialidad;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BE_Usuario> ListarMedicos()
        {
            try
            {
                List<BE_Usuario> _listaMedicos = new List<BE_Usuario>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmdListarMedicos = new SqlCommand(@"Select * from Usuario_Rol ur inner join Usuario N on N.IdUsuario = ur.IdUsuario Where IdRol = 65", conexion);
                    SqlDataReader lector = cmdListarMedicos.ExecuteReader();

                    while (lector.Read())
                    {
                        BE_Usuario usuMedico = new BE_Usuario();
                        usuMedico.IdUsuario = Convert.ToInt32(lector[0].ToString());
                        usuMedico.Nombre = lector[3].ToString();
                        usuMedico.Apellido = lector[4].ToString();
                        _listaMedicos.Add(usuMedico);     
                    }

                    return _listaMedicos;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BE_Usuario> ListarMedicosPorEspecialidad(int idEspecialidad)
        {
            try
            {
                List<BE_Usuario> _listaMedicos = new List<BE_Usuario>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmdListarMedicos = new SqlCommand(@"
                        Select u.IdUsuario, u.Nombre, u.Apellido
                        From Medico_Especialidad me
                        Inner join Usuario u on u.IdUsuario = me.IdMedico
                        Where me.IdEsp = @idEspecialidad
                        Order by u.Apellido, u.Nombre", conexion);
                    cmdListarMedicos.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = idEspecialidad;

                    SqlDataReader lector = cmdListarMedicos.ExecuteReader();

                    while (lector.Read())
                    {
                        BE_Usuario usuMedico = new BE_Usuario();
                        usuMedico.IdUsuario = Convert.ToInt32(lector["IdUsuario"]);
                        usuMedico.Nombre = lector["Nombre"].ToString();
                        usuMedico.Apellido = lector["Apellido"].ToString();
                        _listaMedicos.Add(usuMedico);
                    }

                    return _listaMedicos;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE_Especialidad> ListarEspecialidadesPorMedico(int idMedico)
        {
            try
            {
                List<BE_Especialidad> lista = new List<BE_Especialidad>();
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        Select e.IdEspecialidad, e.Nombre, e.ValorTurno, e.PorcentajeAusente
                        From Medico_Especialidad me
                        Inner join Especialidades e on e.IdEspecialidad = me.IdEsp
                        Where me.IdMedico = @idMedico
                        Order by e.Nombre", conexion);
                    cmd.Parameters.Add("@idMedico", SqlDbType.Int).Value = idMedico;

                    SqlDataReader lector = cmd.ExecuteReader();
                    while (lector.Read())
                    {
                        BE_Especialidad especialidad = new BE_Especialidad();
                        especialidad.IdEspecialidad = Convert.ToInt32(lector["IdEspecialidad"]);
                        especialidad.Nombre = lector["Nombre"].ToString();
                        especialidad.ValorTurno = Convert.ToDecimal(lector["ValorTurno"]);
                        especialidad.PorcentajeAusente = Convert.ToDecimal(lector["PorcentajeAusente"]);
                        lista.Add(especialidad);
                    }
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AsignarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmdAsignarEspecialidad = new SqlCommand(@"Insert into Medico_Especialidad (IdMedico, IdEsp) VALUES (@idUsuMedico, @idEspecialidad)", conexion);

                    cmdAsignarEspecialidad.Parameters.Add("@idUsuMedico", SqlDbType.Int).Value = idMedico;
                    cmdAsignarEspecialidad.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = idEspecialidad;

                    cmdAsignarEspecialidad.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DesasignarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();
                    SqlCommand cmdDesasignarEspecialidad = new SqlCommand(@"Delete from Medico_Especialidad where IdMedico = @idUsuMedico and IdEsp = @idEspecialidad", conexion);
                    cmdDesasignarEspecialidad.Parameters.Add("@idUsuMedico", SqlDbType.Int).Value = idMedico;
                    cmdDesasignarEspecialidad.Parameters.Add("@idEspecialidad", SqlDbType.Int).Value = idEspecialidad;
                    cmdDesasignarEspecialidad.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

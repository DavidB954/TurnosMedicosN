using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class DAL_Roles
    {
        DAL_Conexion conex = new DAL_Conexion();


        //ROLES
        public int InsertarRol(string nombre, string descripcion)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdInsertarRol = new SqlCommand("Insert into Rol (Nombre, Descripcion) OUTPUT INSERTED.IdRol VALUES (@nombre, @descripcion)", conexion);

                cmdInsertarRol.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;
                cmdInsertarRol.Parameters.Add("@descripcion", SqlDbType.VarChar, 100).Value = (object)descripcion ?? DBNull.Value;

                return (int)cmdInsertarRol.ExecuteScalar();
            }
        }

        public void ModificarRol(int idRol, string nombre, string descripcion)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdActRol = new SqlCommand("Update Rol SET Nombre=@nombre, Descripcion=@descripcion WHERE IdRol=@id", conexion);

                cmdActRol.Parameters.Add("@id", SqlDbType.Int).Value = idRol;
                cmdActRol.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;
                cmdActRol.Parameters.Add("@descripcion", SqlDbType.VarChar, 50).Value = descripcion;

                cmdActRol.ExecuteNonQuery();
            }
        }

        //Eliminamos todos los Roles 
        public void EliminarRol(int id)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                //Primero borramos de la tabla Rol_Rol
                SqlCommand cmdBorrarRolRol = new SqlCommand("Delete from Rol_Rol Where IdRolPadre = @id OR IdRolHijo=@id", conexion);

                cmdBorrarRolRol.Parameters.Add("@id", SqlDbType.Int).Value = id;

                cmdBorrarRolRol.ExecuteNonQuery();

                
                //Borramos el Rol de Rol_Permiso
                SqlCommand cmdBorrarRol_Permiso = new SqlCommand("Delete from Rol_Permiso Where IdRol=@id", conexion);

                cmdBorrarRol_Permiso.Parameters.Add("@id", SqlDbType.Int).Value = id;

                cmdBorrarRol_Permiso.ExecuteNonQuery();

                //Eliminamos el Rol
                SqlCommand cmdBorrarRol = new SqlCommand("Delete from Rol Where IdRol=@Id", conexion);
                cmdBorrarRol.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmdBorrarRol.ExecuteNonQuery();

            }
        }

        //Obtenemos todos los roles.

        public DataTable ObtenerTodosLosRoles()
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdObtenerRoles = new SqlCommand("Select * from Rol", conexion);

                SqlDataAdapter da = new SqlDataAdapter(cmdObtenerRoles);

                DataTable dt = new DataTable();

                da.Fill(dt);

                return dt;                
            }        
        }

        public string ObtenerNombreRol(int idRol)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdNombreRol = new SqlCommand("Select Nombre from Rol Where IdRol=@id", conexion);

                cmdNombreRol.Parameters.Add("@id", SqlDbType.Int).Value = idRol;

                object resultado = cmdNombreRol.ExecuteScalar();

                return resultado?.ToString();
            }
        }

        //Verificamos que no exista un nombre con el mismo rol

        public bool ExisteNombreRol(string nombre, int? idExcluir = null)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                var sql = idExcluir.HasValue
                           ? "SELECT COUNT(1) FROM Rol WHERE Nombre=@Nombre AND IdRol<>@Id"
                           : "SELECT COUNT(1) FROM Rol WHERE Nombre=@Nombre";
                SqlCommand cmd = new SqlCommand(sql, conexion);

                cmd.Parameters.AddWithValue("@Nombre", nombre);
                
                if (idExcluir.HasValue)
                    cmd.Parameters.AddWithValue("@Id", idExcluir.Value);
                
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        //-- PERMISO -------------------------------

        public int InsertarPermiso(string nombre)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmdInsertarPermiso = new SqlCommand("Insert into Permisos (Nombre) OUTPUT INSERTED.IdPermisos VALUES (@nombre)", conexion);

                    cmdInsertarPermiso.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;

                    return (int)cmdInsertarPermiso.ExecuteScalar();

                }
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
           
        }

        public string ObtenerNombrePermiso(int idPermiso)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdNombrePermiso = new SqlCommand("Select Nombre from Permisos Where IdPermisos=@id", conexion);

                cmdNombrePermiso.Parameters.Add("@id", SqlDbType.Int).Value = idPermiso;

                object resultado = cmdNombrePermiso.ExecuteScalar();

                return resultado?.ToString();
            }
        }

        public void ModificarPermiso(int idPermiso, string nombre)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdModPermiso = new SqlCommand("Update Permisos SET Nombre=@nombre Where IdPermisos = @id", conexion);

                cmdModPermiso.Parameters.Add("@id", SqlDbType.Int).Value = idPermiso;
                cmdModPermiso.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = nombre;

                cmdModPermiso.ExecuteNonQuery();
            }
        }

        public void EliminarPermisos(int id)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                //Primero borramos de Rol_Permiso

                SqlCommand cmdBorrarPermisoRolPermiso = new SqlCommand("Delete from Rol_Permiso Where IdPermisos =@id", conexion);

                cmdBorrarPermisoRolPermiso.Parameters.Add("@id", SqlDbType.Int).Value = id;

                cmdBorrarPermisoRolPermiso.ExecuteNonQuery();

                //Borramos el permiso de la tabla Permisos

                SqlCommand cmdBorrarPermisos = new SqlCommand("Delete from Permisos Where IdPermisos=@id", conexion);

                cmdBorrarPermisos.Parameters.Add("@id", SqlDbType.Int).Value = id;

                cmdBorrarPermisos.ExecuteNonQuery();
            }
        }


        // Traemos todos los permisos

        public DataTable ObtenerTodosLosPermisos()
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                SqlCommand cmdObtenerPermisos = new SqlCommand("Select * from Permisos", conexion);
                SqlDataAdapter da = new SqlDataAdapter(cmdObtenerPermisos);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        //Verificar que no exista el nombre del permiso
        public bool ExisteNombrePermiso(string nombre, int ? idExcluir =null)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                var sql = idExcluir.HasValue
                    ? "SELECT COUNT(1) FROM Permisos WHERE Nombre=@Nombre AND IdPermisos<>@Id"
                    : "SELECT COUNT(1) FROM Permisos WHERE Nombre=@Nombre";
                SqlCommand cmdExisteNombre = new SqlCommand(sql, conexion);

                cmdExisteNombre.Parameters.Add("@Nombre", SqlDbType.VarChar, 30).Value = nombre;
                if (idExcluir.HasValue)
                {
                    cmdExisteNombre.Parameters.AddWithValue("@Id", idExcluir.Value);
                }
                return (int)cmdExisteNombre.ExecuteScalar() > 0;
            }
        }



        ///Jerarquia ROL-ROL -- Padre - HIJO
      
        public void AsignarRolHijo(int idPadre, int idHijo)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdRolHijo = new SqlCommand(@"IF NOT EXISTS (SELECT 1 FROM Rol_Rol WHERE IdRolPadre=@idRolPadre AND IdRolHijo=@idRolHijo)
                        Insert into Rol_Rol (IdRolPadre, IdRolHijo) VALUES (@idRolPadre, @idRolHijo)", conexion);

                cmdRolHijo.Parameters.Add("@idRolPadre", SqlDbType.Int).Value = idPadre;

                cmdRolHijo.Parameters.Add("@idRolHijo", SqlDbType.Int).Value = idHijo;

                cmdRolHijo.ExecuteNonQuery();
            }
        }

        public void DesasignarRolHijo(int idPadre, int idHijo)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdDesasignarHijo = new SqlCommand("Delete from Rol_Rol Where IdRolPadre=@idRolP AND IdRolHijo=@idRolH", conexion);

                cmdDesasignarHijo.Parameters.Add("@idRolP", SqlDbType.Int).Value = idPadre;
                cmdDesasignarHijo.Parameters.Add("@idRolH", SqlDbType.Int).Value = idHijo;

                cmdDesasignarHijo.ExecuteNonQuery();
            }
        }

        public List<int> ObtenerHijosDeRol(int idPadre)
        {
            var Lista = new List<int>();

            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdLista = new SqlCommand("Select IdRolHijo from Rol_Rol Where IdRolPadre = @idRolPadre", conexion);

                cmdLista.Parameters.Add("@idRolPadre", SqlDbType.Int).Value = idPadre;

                SqlDataReader lector = cmdLista.ExecuteReader();

                while (lector.Read())
                {
                    Lista.Add(lector.GetInt32(0));
                }


                return Lista;
            }
        }

        // Rol_Permiso

        public void AsignarPermisoARol(int idRol, int idPermiso)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdAsignarP = new SqlCommand(@"IF NOT EXISTS (SELECT 1 FROM Rol_Permiso WHERE IdRol=@rol and IdPermisos=@permiso)
                                   INSERT INTO Rol_Permiso (IdRol, IdPermisos) VALUES (@rol, @permiso)", conexion);


                cmdAsignarP.Parameters.Add("@rol", SqlDbType.Int).Value = idRol;
                cmdAsignarP.Parameters.Add("@permiso", SqlDbType.Int).Value = idPermiso;

                cmdAsignarP.ExecuteNonQuery();
            }

        }

        public void DesasignarPermisoDeRol(int idRol, int idPermisos)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdDesasignarP = new SqlCommand("Delete from Rol_Permiso Where IdRol=@rol and IdPermisos=@permisos", conexion);
                cmdDesasignarP.Parameters.Add("@rol", SqlDbType.Int).Value = idRol;
                cmdDesasignarP.Parameters.Add("@permisos", SqlDbType.Int).Value = idPermisos;

                cmdDesasignarP.ExecuteNonQuery();
            }
        }

        //public List<int> ObtenerPermisosDe(int idRol)
        //{
        //    var Lista = new List<int>();

        //    using (SqlConnection conexion = conex.ObtenerConexion())
        //    {
        //        conexion.Open();

        //        SqlCommand cmdLista = new SqlCommand("Select IdPermisos from Rol_Permisos Where IdRol = @idRol", conexion);

        //        cmdLista.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;

        //        SqlDataReader Lector = cmdLista.ExecuteReader();

        //        while (Lector.Read())
        //        {
        //            Lista.Add(Lector.GetInt32(0));
        //        }

        //        return Lista;
        //    }
        //}

        // Carga completa del Arbol

        public (DataTable Roles, DataTable Permisos, DataTable RolRol, DataTable RolPermisos) CargarArbol()
        {
            using (SqlConnection conexion= conex.ObtenerConexion())
            {
                conexion.Open();

                DataTable roles = new DataTable(), permisos = new DataTable(), rolrol = new DataTable(), rolpermiso = new DataTable();

                new SqlDataAdapter("SELECT * FROM Rol", conexion).Fill(roles);
                new SqlDataAdapter("SELECT * FROM Permisos", conexion).Fill(permisos);
                new SqlDataAdapter("SELECT * FROM Rol_Rol", conexion).Fill(rolrol);
                new SqlDataAdapter("SELECT * FROM Rol_Permiso", conexion).Fill(rolpermiso);

                return (roles, permisos, rolrol, rolpermiso);
            }

        }

        public void AgregarRolUsuario(int idUsuario, int idRol)
        {
            try
            {
                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();

                    SqlCommand cmdRolUsuario = new SqlCommand(@"
                IF EXISTS (SELECT 1 FROM Usuario_Rol WHERE IdUsuario=@idUsu AND IdRol=@idRol)
                    RAISERROR('El usuario ya tiene ese rol asignado.', 16, 1)
                ELSE
                    INSERT INTO Usuario_Rol (IdUsuario, IdRol) VALUES (@idUsu, @idRol)", conexion);

                    cmdRolUsuario.Parameters.Add("@idUsu", SqlDbType.Int).Value = idUsuario;
                    cmdRolUsuario.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;

                    cmdRolUsuario.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
          
        }

        public DataTable ObtenerRolesPorUsuario(int idUsuario)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT IdRol FROM Usuario_Rol WHERE IdUsuario = @id", conexion);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void QuitarRolUsuario(int idUsuario, int idRol)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdQuitarRol = new SqlCommand("Delete from Usuario_Rol Where IdUsuario=@idUsu AND IdRol=@idRol", conexion);

                cmdQuitarRol.Parameters.Add("@idUsu", SqlDbType.Int).Value = idUsuario;
                cmdQuitarRol.Parameters.Add("@idRol", SqlDbType.Int).Value = idRol;

                cmdQuitarRol.ExecuteNonQuery();

            }
        }
        //public void LimpiarTablas()
        //{
        //    using (SqlConnection conexion = conex.ObtenerConexion())
        //    {
        //        conexion.Open();
        //        new SqlCommand("DELETE FROM Rol_Permiso", conexion).ExecuteNonQuery();
        //        new SqlCommand("DELETE FROM Rol_Rol", conexion).ExecuteNonQuery();
        //        new SqlCommand("DELETE FROM Permisos", conexion).ExecuteNonQuery();
        //        new SqlCommand("DELETE FROM Rol", conexion).ExecuteNonQuery();
        //    }
        //}


    }
}

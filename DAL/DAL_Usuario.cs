using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAL_Usuario
    {
        DAL_Conexion conex = new DAL_Conexion();


        //Obtenemos el usuario por id
        public BE_Usuario ObtenerUsuarioPorId(int IdUsuario)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                SqlCommand cmdUsuId = new SqlCommand("Select * From Usuario Where IdUsuario=@id", conexion);
                cmdUsuId.Parameters.Add("@id", SqlDbType.Int).Value = IdUsuario;
                SqlDataReader lector = cmdUsuId.ExecuteReader();
                if (!lector.Read())
                    return null;

                return new BE_Usuario
                {
                    IdUsuario = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Apellido = lector.GetString(2),
                    Email = lector.GetString(3),
                    HashPassword = lector.GetString(4),
                    Activo = lector.GetBoolean(5),
                    IntentosFallidos = lector.GetInt32(6),
                    DVH = lector.GetString(7)
                };
            }
        }


        //Verificamos que existe el mail del Usuario y devolvemos un objeto Usuario con la contraseña hasheada para poder compararla luego.

        public BE_Usuario ObtenerUsuarioPorEmail(string email)
        {

            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdUsuEmail = new SqlCommand("Select * From Usuario Where Email=@email", conexion);

                cmdUsuEmail.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = email;

                SqlDataReader Lector = cmdUsuEmail.ExecuteReader();


                if (!Lector.Read())
                {
                    return null;
                }

                else return new BE_Usuario
                {
                    IdUsuario = Lector.GetInt32(0),
                    Nombre = Lector.GetString(1),
                    Apellido = Lector.GetString(2),
                    Email = Lector.GetString(3),
                    HashPassword = Lector.GetString(4),
                    Activo = Lector.GetBoolean(5),
                    IntentosFallidos = Lector.GetInt32(6),
                    DVH = Lector.GetString(7)
                };
            }
        }

        //Updateamos los intentos fallidos en caso de que el login sea incorrecto, y bloqueamos el usuario si supera los 3 intentos fallidos.
        public void ActualizarIntentosFallidos(int intentos, int IdUsuario, string DVH)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdActualizarIntentos = new SqlCommand("Update Usuario Set IntentosFallidos = @intentos, DVH = @dvh Where IdUsuario=@id", conexion);

                cmdActualizarIntentos.Parameters.Add("@intentos", SqlDbType.Int).Value = intentos;
                cmdActualizarIntentos.Parameters.Add("@id", SqlDbType.Int).Value = IdUsuario;
                cmdActualizarIntentos.Parameters.Add("@dvh", SqlDbType.VarChar, 255).Value = DVH;

                cmdActualizarIntentos.ExecuteNonQuery();
            }
        }

        public void BloquearUsuario(int IdUsuario, string DVH)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand cmdBloquearUsuario = new SqlCommand("Update Usuario SET Activo=0, DVH=@dvh Where IdUsuario = @id", conexion);

                cmdBloquearUsuario.Parameters.Add("@id", SqlDbType.Int).Value = IdUsuario;
                cmdBloquearUsuario.Parameters.Add("@dvh", SqlDbType.VarChar, 255).Value = DVH;

                cmdBloquearUsuario.ExecuteNonQuery();
            }
        }
        public List<BE_Usuario> ListaUsuario()
        {
            try
            {
                List<BE_Usuario> ListaUsu = new List<BE_Usuario>();

                using (SqlConnection conexion = conex.ObtenerConexion())
                {
                    conexion.Open();

                    SqlCommand comando = new SqlCommand("Select IdUsuario, Nombre, Apellido, Email, HashPassword, Activo, IntentosFallidos, DVH from Usuario", conexion);

                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        BE_Usuario Usu = new BE_Usuario();
                        Usu.IdUsuario = Convert.ToInt32(lector[0]);
                        Usu.Nombre = lector[1].ToString();
                        Usu.Apellido = lector[2].ToString();
                        Usu.Email = lector[3].ToString();
                        Usu.HashPassword = lector[4].ToString();
                        Usu.Activo = Convert.ToBoolean(lector[5]);
                        Usu.IntentosFallidos = Convert.ToInt32(lector[6]);
                        Usu.DVH = lector[7].ToString();
                        ListaUsu.Add(Usu);
                    }

                    return ListaUsu;
                }
            }
            catch (Exception)
            {
                return null;                
            }
        }

        public void AgregarUsuario(BE_Usuario Usuario)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("Insert into Usuario (Nombre, Apellido, Email, HashPassword, DVH) VALUES (@nombre, @apellido, @email, @hash, @dvh)", conexion);

                comando.Parameters.AddWithValue("@nombre", Usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", Usuario.Apellido);
                comando.Parameters.AddWithValue("@email", Usuario.Email);
                comando.Parameters.AddWithValue("@hash", Usuario.HashPassword);
                comando.Parameters.AddWithValue("@dvh", Usuario.DVH);

                comando.ExecuteNonQuery();
            }

        }

        public void ModificarUsuario(BE_Usuario Usuario)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("Update Usuario SET Nombre=@nombre, Apellido=@apellido, Email=@email, HashPassword=@hash, Activo = @activo, DVH = @dvh WHERE IdUsuario=@id", conexion);

                comando.Parameters.AddWithValue("@id", Usuario.IdUsuario);
                comando.Parameters.AddWithValue("@nombre", Usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", Usuario.Apellido);
                comando.Parameters.AddWithValue("@email", Usuario.Email);
                comando.Parameters.AddWithValue("@hash", Usuario.HashPassword);
                comando.Parameters.Add("@activo", SqlDbType.Bit).Value = Usuario.Activo;
                comando.Parameters.Add("@dvh", SqlDbType.VarChar, 255).Value = Usuario.DVH;
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarUsuario(int id)
        {
            using (SqlConnection conexion = conex.ObtenerConexion())
            {
                conexion.Open();
                //Primero borro todo lo de ese usuario en la bitacora
                SqlCommand cmdBorrarUsuBitacora = new SqlCommand("Delete from Bitacora WHERE IdUsuario=@idUsuBorrado", conexion);

                cmdBorrarUsuBitacora.Parameters.Add("@idUsuBorrado", SqlDbType.Int).Value = id;

                cmdBorrarUsuBitacora.ExecuteNonQuery();

                SqlCommand comando = new SqlCommand("Delete from Usuario WHERE IdUsuario = @id", conexion);

                comando.Parameters.AddWithValue("@id", id);

                comando.ExecuteNonQuery();
            }
        }
    }
}

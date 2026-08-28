using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BE.BE_Traduccion;

namespace DAL
{
    public class DAL_Traduccion
    {

        private DAL_Conexion cn = new DAL_Conexion();

        public List<Idioma> ObtenerIdiomas()
        {
            var lista = new List<Idioma>();
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("SELECT * FROM Idiomas", con))
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                    lista.Add(new Idioma
                    {
                        Id = (int)dr["Id"],
                        Codigo = dr["Codigo"].ToString(),
                        Nombre = dr["Nombre"].ToString()
                    });
            }
            return lista;
        }

        public List<Traduccion> ObtenerPorIdioma(int idIdioma)
        {
            var lista = new List<Traduccion>();
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("SELECT * FROM Traducciones WHERE IdIdioma = @id", con))
            {
                cmd.Parameters.AddWithValue("@id", idIdioma);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                    lista.Add(new Traduccion
                    {
                        Id = (int)dr["Id"],
                        Clave = dr["Clave"].ToString(),
                        IdIdioma = (int)dr["IdIdioma"],
                        Texto = dr["Traduccion"].ToString()
                    });
            }
            return lista;
        }

        public List<Traduccion> ObtenerTodas()
        {
            var lista = new List<Traduccion>();
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("SELECT * FROM Traducciones", con))
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                    lista.Add(new Traduccion
                    {
                        Id = (int)dr["Id"],
                        Clave = dr["Clave"].ToString(),
                        IdIdioma = (int)dr["IdIdioma"],
                        Texto = dr["Traduccion"].ToString()
                    });
            }
            return lista;
        }

        public void Insertar(Traduccion t)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("INSERT INTO Traducciones (Clave, IdIdioma, Traduccion) VALUES (@c, @i, @t)", con))
            {
                cmd.Parameters.AddWithValue("@c", t.Clave);
                cmd.Parameters.AddWithValue("@i", t.IdIdioma);
                cmd.Parameters.AddWithValue("@t", t.Texto);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Traduccion t)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("UPDATE Traducciones SET Traduccion=@t WHERE Id=@id", con))
            {
                cmd.Parameters.AddWithValue("@id", t.Id);
                cmd.Parameters.AddWithValue("@t", t.Texto);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("DELETE FROM Traducciones WHERE Id=@id", con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertarIdioma(Idioma idioma)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("INSERT INTO Idiomas (Codigo, Nombre) VALUES (@c, @n)", con))
            {
                cmd.Parameters.AddWithValue("@c", idioma.Codigo);
                cmd.Parameters.AddWithValue("@n", idioma.Nombre);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarIdioma(int idIdioma)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("DELETE FROM Idiomas WHERE Id=@id", con))
            {
                cmd.Parameters.AddWithValue("@id", idIdioma);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarTraduccionesPorIdioma(int idIdioma)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("DELETE FROM Traducciones WHERE IdIdioma=@id", con))
            {
                cmd.Parameters.AddWithValue("@id", idIdioma);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GuardarIdiomaUsuario(int idUsuario, int idIdioma)
        {
            try
            {
                using (var con = cn.ObtenerConexion())
                using (var cmd = new SqlCommand("UPDATE Usuario SET IdIdioma=@i WHERE IdUsuario=@u", con))
                {
                    cmd.Parameters.AddWithValue("@i", idIdioma);
                    cmd.Parameters.AddWithValue("@u", idUsuario);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            
        }

        public Idioma ObtenerIdiomaPorUsuario(int idUsuario)
        {
            using (var con = cn.ObtenerConexion())
            using (var cmd = new SqlCommand("SELECT I.* FROM Idiomas I JOIN Usuario U ON U.IdIdioma = I.Id WHERE U.IdUsuario = @u", con))
            {
                cmd.Parameters.AddWithValue("@u", idUsuario);
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                    return new Idioma
                    {
                        Id = (int)dr["Id"],
                        Codigo = dr["Codigo"].ToString(),
                        Nombre = dr["Nombre"].ToString()
                    };
            }
            return null;
        }
    }
}

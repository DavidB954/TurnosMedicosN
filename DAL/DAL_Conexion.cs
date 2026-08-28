using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Conexion
    {
        private static readonly string ConnectionString = "Data Source=.; Initial Catalog= GestionTurnosMedicos; Integrated Security = True";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(ConnectionString);        
        }

    }
}

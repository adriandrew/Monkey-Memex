using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Entidades
{
    public static class BaseDatos
    {

        public static SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());

    }
}

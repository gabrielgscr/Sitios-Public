using System;
using System.Configuration;
using System.Data.SqlClient;

namespace EjemploWebService.AccesoDatos
{
    public abstract class RepositorioBase
    {
        protected RepositorioBase(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("La string de conexión no puede estar vacía.", nameof(connectionString));
            }

            ConnectionString = connectionString;
        }

        protected string ConnectionString { get; }

        protected static string ObtenerCadenaConexionDesdeConfig(string connectionStringName)
        {
            var setting = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
            {
                throw new ConfigurationErrorsException($"No se encontró la string de conexión '{connectionStringName}'.");
            }

            return setting.ConnectionString;
        }

        protected SqlConnection CrearConexion()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
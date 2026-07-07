using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EjemploWebService.Modelos;

namespace EjemploWebService.AccesoDatos
{
    public class TelefonoRepositorio : RepositorioBase, ITelefonoRepositorio
    {
        public TelefonoRepositorio(string connectionString) : base(connectionString)
        {
        }

        public List<Telefono> ObtenerPorPersona(string personaID)
        {
            if (string.IsNullOrWhiteSpace(personaID))
            {
                throw new ArgumentException("El identificador de la persona es obligatorio.", nameof(personaID));
            }

            var telefonos = new List<Telefono>();

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
SELECT TelefonoID, PersonaID, Telefono
FROM Telefono
WHERE PersonaID = @PersonaID
ORDER BY TelefonoID;";
                comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });

                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        telefonos.Add(MapearTelefono(lector));
                    }
                }
            }

            return telefonos;
        }

        private static Telefono MapearTelefono(SqlDataReader lector)
        {
            return new Telefono
            {
                TelefonoID = lector.GetInt32(lector.GetOrdinal("TelefonoID")),
                PersonaID = lector.GetString(lector.GetOrdinal("PersonaID")),
                NumeroTelefono = lector.GetString(lector.GetOrdinal("Telefono"))
            };
        }
    }
}
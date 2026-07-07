using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EjemploWebService.Modelos;

namespace EjemploWebService.AccesoDatos
{
    public class RolRepositorio : RepositorioBase, IRolRepositorio
    {
        public RolRepositorio(string connectionString) : base(connectionString)
        {
        }

        public List<Rol> ObtenerTodos()
        {
            var roles = new List<Rol>();

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
SELECT RolID, Nombre
FROM Rol
ORDER BY Nombre;";

                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        roles.Add(MapearRol(lector));
                    }
                }
            }

            return roles;
        }

        public List<Rol> ObtenerPorPersona(string personaID)
        {
            if (string.IsNullOrWhiteSpace(personaID))
            {
                throw new ArgumentException("El identificador de la persona es obligatorio.", nameof(personaID));
            }

            var roles = new List<Rol>();

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
SELECT r.RolID, r.Nombre
FROM PersonaRol pr
INNER JOIN Rol r ON r.RolID = pr.RolID
WHERE pr.PersonaID = @PersonaID
ORDER BY r.Nombre;";
                comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });

                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        roles.Add(MapearRol(lector));
                    }
                }
            }

            return roles;
        }

        private static Rol MapearRol(SqlDataReader lector)
        {
            return new Rol
            {
                RolID = lector.GetInt32(lector.GetOrdinal("RolID")),
                Nombre = lector.GetString(lector.GetOrdinal("Nombre"))
            };
        }
    }
}
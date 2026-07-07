using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EjemploWebService.Modelos;

namespace EjemploWebService.AccesoDatos
{
    public class PersonaRepositorio : RepositorioBase, IPersonaRepositorio
    {
        public PersonaRepositorio(string connectionString) : base(connectionString)
        {
        }

        public List<Persona> ObtenerTodas()
        {
            var personas = new List<Persona>();

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
SELECT PersonaID, Nombre, Tipo, Gender
FROM Persona
ORDER BY Nombre;";

                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        personas.Add(MapearPersona(lector));
                    }
                }
            }

            return personas;
        }

        public Persona ObtenerPorId(string personaID)
        {
            if (string.IsNullOrWhiteSpace(personaID))
            {
                throw new ArgumentException("El identificador de la persona es obligatorio.", nameof(personaID));
            }

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
SELECT PersonaID, Nombre, Tipo, Gender
FROM Persona
WHERE PersonaID = @PersonaID;";
                comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });

                conexion.Open();
                using (var lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearPersona(lector) : null;
                }
            }
        }

        public bool Existe(string personaID)
        {
            if (string.IsNullOrWhiteSpace(personaID))
            {
                return false;
            }

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = "SELECT COUNT(1) FROM Persona WHERE PersonaID = @PersonaID;";
                comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });

                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        public void Insertar(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
INSERT INTO Persona (PersonaID, Nombre, Tipo, Gender, Password)
VALUES (@PersonaID, @Nombre, @Tipo, @Gender, @Password);";
                comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = persona.PersonaID });
                comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 50) { Value = persona.Nombre });
                comando.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.TinyInt) { Value = persona.Tipo });
                comando.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 10) { Value = persona.Gender });
                comando.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = persona.Password });

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Actualizar(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            using (var conexion = CrearConexion())
            using (var comando = conexion.CreateCommand())
            {
                comando.CommandText = @"
UPDATE Persona
SET Nombre = @Nombre,
    Tipo = @Tipo,
    Gender = @Gender,
    Password = @Password
WHERE PersonaID = @PersonaID;";
                comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = persona.PersonaID });
                comando.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 50) { Value = persona.Nombre });
                comando.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.TinyInt) { Value = persona.Tipo });
                comando.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 10) { Value = persona.Gender });
                comando.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 100) { Value = persona.Password });

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Eliminar(string personaID)
        {
            if (string.IsNullOrWhiteSpace(personaID))
            {
                throw new ArgumentException("El identificador de la persona es obligatorio.", nameof(personaID));
            }

            using (var conexion = CrearConexion())
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction())
                using (var comando = conexion.CreateCommand())
                {
                    comando.Transaction = transaccion;

                    comando.CommandText = "DELETE FROM PersonaRol WHERE PersonaID = @PersonaID;";
                    comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });
                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                    comando.CommandText = "DELETE FROM Telefono WHERE PersonaID = @PersonaID;";
                    comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });
                    comando.ExecuteNonQuery();

                    comando.Parameters.Clear();
                    comando.CommandText = "DELETE FROM Persona WHERE PersonaID = @PersonaID;";
                    comando.Parameters.Add(new SqlParameter("@PersonaID", SqlDbType.VarChar, 50) { Value = personaID });
                    comando.ExecuteNonQuery();

                    transaccion.Commit();
                }
            }
        }

        private static Persona MapearPersona(SqlDataReader lector)
        {
            return new Persona
            {
                PersonaID = lector.GetString(lector.GetOrdinal("PersonaID")),
                Nombre = lector.GetString(lector.GetOrdinal("Nombre")),
                Tipo = lector.GetByte(lector.GetOrdinal("Tipo")),
                Gender = lector.GetString(lector.GetOrdinal("Gender"))
            };
        }
    }
}
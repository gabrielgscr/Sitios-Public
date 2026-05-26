using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ASPNetSample.Entities;
using System.Configuration;

namespace ASPNetSample.DataAccess
{
    public class PersonaDA
    {
        private readonly string _connectionString;

        public PersonaDA()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["cnx"].ConnectionString;
        }

        public IEnumerable<Persona> GetAllPersonas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<Persona>("SELECT PersonaID, Nombre, Tipo, Gender, Password FROM [dbo].[Persona]");
            }
        }

        public Persona GetPersonaById(string personaId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Persona>("SELECT PersonaID, Nombre, Tipo, Gender, Password FROM [dbo].[Persona] WHERE PersonaID = @PersonaID", new { PersonaID = personaId });
            }
        }

        public void InsertPersona(Persona persona)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO [dbo].[Persona] (PersonaID, Nombre, Tipo, Gender, Password) VALUES (@PersonaID, @Nombre, @Tipo, @Gender, @Password)", persona);
            }
        }

        public void UpdatePersona(Persona persona)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("UPDATE [dbo].[Persona] SET Nombre = @Nombre, Tipo = @Tipo, Gender = @Gender, Password = @Password WHERE PersonaID = @PersonaID", persona);
            }
        }

        public void DeletePersona(string personaId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("DELETE FROM [dbo].[Persona] WHERE PersonaID = @PersonaID", new { PersonaID = personaId });
            }
        }
    }
}


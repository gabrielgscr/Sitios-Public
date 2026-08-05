using Dapper;
using EjemploMicroServicioPersona.Entities;

namespace EjemploMicroServicioPersona.Repository
{
    public class PersonaRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PersonaRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QueryAsync<Persona>(
                    "SELECT PersonaID AS PersonaId, Nombre, Tipo, Gender FROM Persona");
            }
        }

        public async Task<Persona?> GetByIdAsync(string id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "SELECT PersonaID AS PersonaId, Nombre, Tipo, Gender FROM Persona WHERE PersonaID = @id";
                return await connection.QueryFirstOrDefaultAsync<Persona>(sql, new { id });
            }
        }

        //Crear una persona
        public async Task<int> CreateAsync(Persona persona)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"INSERT INTO Persona (PersonaID, Nombre, Tipo, Gender, Password)
                            VALUES (@PersonaId, @Nombre, @Tipo, @Gender, @Password)";
                return await connection.ExecuteAsync(sql, persona);
            }
        }

        public async Task<int> UpdateAsync(Persona persona)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = @"UPDATE Persona
                            SET Nombre = @Nombre,
                                Tipo = @Tipo,
                                Gender = @Gender
                          WHERE PersonaID = @PersonaId";
                return await connection.ExecuteAsync(sql, persona);
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM Persona WHERE PersonaID = @id";
                return await connection.ExecuteAsync(sql, new { id });
            }
        }
    }
}

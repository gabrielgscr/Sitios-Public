using CoreWebSample.Entities;
using Dapper;

namespace CoreWebSample.Repository
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
                return await connection.QueryAsync<Persona>("SELECT PersonaID, Nombre, Tipo, Gender, Password FROM Persona");
            }
        }

        public async Task<Persona?> GetByIdAsync(string id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Persona>("SELECT PersonaID, Nombre, Tipo, Gender, Password FROM Persona WHERE PersonaID = @Id", new { Id = id });
            }
        }

        public async Task<int> InsertAsync(Persona persona)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "INSERT INTO Persona (PersonaID, Nombre, Tipo, Gender, Password) VALUES (@PersonaID, @Nombre, @Tipo, @Gender, @Password)";
                return await connection.ExecuteAsync(sql, persona);
            }
        }

        public async Task<int> UpdateAsync(Persona persona)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "UPDATE Persona SET Nombre = @Nombre, Tipo = @Tipo, Gender = @Gender, Password = @Password WHERE PersonaID = @PersonaID";
                return await connection.ExecuteAsync(sql, persona);
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var sql = "DELETE FROM Persona WHERE PersonaID = @Id";
                return await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}

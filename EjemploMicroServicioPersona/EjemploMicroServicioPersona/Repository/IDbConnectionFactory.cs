using System.Data;

namespace EjemploMicroServicioPersona.Repository
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

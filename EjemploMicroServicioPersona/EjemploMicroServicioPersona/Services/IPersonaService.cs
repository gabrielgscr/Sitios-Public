using EjemploMicroServicioPersona.Entities;

namespace EjemploMicroServicioPersona.Services
{
    public interface IPersonaService
    {
        Task<IEnumerable<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(string id);
        //Crear una persona
        Task<int> CreateAsync(Persona persona);
        Task<int> UpdateAsync(Persona persona);
        Task<int> DeleteAsync(string id);
    }
}

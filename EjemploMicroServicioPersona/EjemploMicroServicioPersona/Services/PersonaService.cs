using EjemploMicroServicioPersona.Entities;
using EjemploMicroServicioPersona.Repository;

namespace EjemploMicroServicioPersona.Services
{
    public class PersonaService : IPersonaService
    {

        private readonly PersonaRepository _personaRepository;
        public PersonaService(PersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _personaRepository.GetAllAsync();
        }

        public async Task<PagedResult<Persona>> GetPageAsync(int pageNumber, int pageSize)
        {
            return await _personaRepository.GetPageAsync(pageNumber, pageSize);
        }

        public async Task<Persona?> GetByIdAsync(string id)
        {
            return await _personaRepository.GetByIdAsync(id);
        }

        //Crear una persona
        public async Task<int> CreateAsync(Persona persona)
        {
            return await _personaRepository.CreateAsync(persona);
        }

        public async Task<int> UpdateAsync(Persona persona)
        {
            return await _personaRepository.UpdateAsync(persona);
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _personaRepository.DeleteAsync(id);
        }

    }
}

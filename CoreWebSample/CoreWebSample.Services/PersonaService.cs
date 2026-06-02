using CoreWebSample.Entities;
using CoreWebSample.Repository;
using CoreWebSample.Services.Abstract;

namespace CoreWebSample.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly PersonaRepository _personaRepository;

        public PersonaService(PersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public Task<IEnumerable<Persona>> GetAllAsync()
        {
            return _personaRepository.GetAllAsync();
        }

        public Task<Persona?> GetByIdAsync(string id)
        {
            return _personaRepository.GetByIdAsync(id);
        }

        public Task<int> InsertAsync(Persona persona)
        {
            return _personaRepository.InsertAsync(persona);
        }

        public Task<int> UpdateAsync(Persona persona)
        {
            return _personaRepository.UpdateAsync(persona);
        }

        public Task<int> DeleteAsync(string id)
        {
            return _personaRepository.DeleteAsync(id);
        }
    }
}

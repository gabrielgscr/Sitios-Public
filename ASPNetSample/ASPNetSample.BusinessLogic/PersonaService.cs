using ASPNetSample.DataAccess;
using ASPNetSample.Entities;
using System.Collections.Generic;

namespace ASPNetSample.BusinessLogic
{
    public class PersonaService
    {
        private readonly PersonaDA _personaDA;

        public PersonaService()
        {
            _personaDA = new PersonaDA();
        }

        public IEnumerable<Persona> GetAllPersonas()
        {
            return _personaDA.GetAllPersonas();
        }

        public Persona GetPersonaById(string personaId)
        {
            return _personaDA.GetPersonaById(personaId);
        }

        public void AddPersona(Persona persona)
        {
            _personaDA.InsertPersona(persona);
        }

        public void UpdatePersona(Persona persona)
        {
            _personaDA.UpdatePersona(persona);
        }

        public void DeletePersona(string personaId)
        {
            _personaDA.DeletePersona(personaId);
        }
    }
}


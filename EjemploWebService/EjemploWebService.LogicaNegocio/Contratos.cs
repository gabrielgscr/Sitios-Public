using System.Collections.Generic;
using EjemploWebService.Modelos;

namespace EjemploWebService.LogicaNegocio
{
    public interface IPersonaServicio
    {
        List<Persona> ObtenerPersonas();
        Persona ObtenerPersona(string personaID);
        List<Rol> ObtenerRoles();
        List<Telefono> ObtenerTelefonosPorPersona(string personaID);
        void GuardarPersona(Persona persona);
        void EliminarPersona(string personaID);
    }
}
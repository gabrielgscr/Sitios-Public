using System.Collections.Generic;
using EjemploWebService.Modelos;

namespace EjemploWebService.AccesoDatos
{
    public interface IPersonaRepositorio
    {
        List<Persona> ObtenerTodas();
        Persona ObtenerPorId(string personaID);
        bool Existe(string personaID);
        void Insertar(Persona persona);
        void Actualizar(Persona persona);
        void Eliminar(string personaID);
    }

    public interface IRolRepositorio
    {
        List<Rol> ObtenerTodos();
        List<Rol> ObtenerPorPersona(string personaID);
    }

    public interface ITelefonoRepositorio
    {
        List<Telefono> ObtenerPorPersona(string personaID);
    }
}
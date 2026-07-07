using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EjemploWebService.LogicaNegocio;
using EjemploWebService.Modelos;

namespace EjemploWebService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioPersonas" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioPersonas.svc o ServicioPersonas.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioPersonas : IServicioPersonas
    {
        private readonly IPersonaServicio _personaServicio;

        public ServicioPersonas()
            : this(CrearServicio())
        {
        }

        internal ServicioPersonas(IPersonaServicio personaServicio)
        {
            _personaServicio = personaServicio;
        }

        public List<Persona> ObtenerPersonas()
        {
            return _personaServicio.ObtenerPersonas();
        }

        public Persona ObtenerPersona(string personaID)
        {
            return _personaServicio.ObtenerPersona(personaID);
        }

        public List<Rol> ObtenerRoles()
        {
            return _personaServicio.ObtenerRoles();
        }

        public List<Telefono> ObtenerTelefonosPorPersona(string personaID)
        {
            return _personaServicio.ObtenerTelefonosPorPersona(personaID);
        }

        public void GuardarPersona(Persona persona)
        {
            _personaServicio.GuardarPersona(persona);
        }

        public void EliminarPersona(string personaID)
        {
            _personaServicio.EliminarPersona(personaID);
        }

        private static IPersonaServicio CrearServicio()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["EjemploWebServiceDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ConfigurationErrorsException("No se encontró la string de conexión 'EjemploWebServiceDb'.");
            }

            return new PersonaServicio(connectionString);
        }
    }
}

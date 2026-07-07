using System;
using System.Collections.Generic;
using System.ServiceModel;
using EjemploWebService;
using EjemploWebService.Modelos;

namespace EjemploWebService.PruebasWeb.Servicios
{
    internal static class ServicioPersonasCliente
    {
        public static List<Persona> ObtenerPersonas(string urlServicio)
        {
            return Ejecutar(urlServicio, cliente => cliente.ObtenerPersonas());
        }

        public static Persona ObtenerPersona(string urlServicio, string personaID)
        {
            return Ejecutar(urlServicio, cliente => cliente.ObtenerPersona(personaID));
        }

        public static List<Rol> ObtenerRoles(string urlServicio)
        {
            return Ejecutar(urlServicio, cliente => cliente.ObtenerRoles());
        }

        public static List<Telefono> ObtenerTelefonosPorPersona(string urlServicio, string personaID)
        {
            return Ejecutar(urlServicio, cliente => cliente.ObtenerTelefonosPorPersona(personaID));
        }

        public static void GuardarPersona(string urlServicio, Persona persona)
        {
            Ejecutar(urlServicio, cliente =>
            {
                cliente.GuardarPersona(persona);
                return true;
            });
        }

        public static void EliminarPersona(string urlServicio, string personaID)
        {
            Ejecutar(urlServicio, cliente =>
            {
                cliente.EliminarPersona(personaID);
                return true;
            });
        }

        private static T Ejecutar<T>(string urlServicio, Func<IServicioPersonas, T> accion)
        {
            if (string.IsNullOrWhiteSpace(urlServicio))
            {
                throw new ArgumentException("Ingresa la URL del servicio.", nameof(urlServicio));
            }

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                MaxReceivedMessageSize = 1024 * 1024,
                MaxBufferSize = 1024 * 1024
            };
            var endpoint = new EndpointAddress(urlServicio);
            var factory = new ChannelFactory<IServicioPersonas>(binding, endpoint);
            var channel = factory.CreateChannel();
            var communicationObject = (ICommunicationObject)channel;

            try
            {
                communicationObject.Open();
                var resultado = accion(channel);
                communicationObject.Close();
                factory.Close();
                return resultado;
            }
            catch
            {
                communicationObject.Abort();
                factory.Abort();
                throw;
            }
        }
    }
}

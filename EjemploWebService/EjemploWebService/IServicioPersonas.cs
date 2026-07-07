using System.Collections.Generic;
using System.ServiceModel;
using EjemploWebService.Modelos;

namespace EjemploWebService
{
    [ServiceContract]
    public interface IServicioPersonas
    {
        [OperationContract]
        List<Persona> ObtenerPersonas();

        [OperationContract]
        Persona ObtenerPersona(string personaID);

        [OperationContract]
        List<Rol> ObtenerRoles();

        [OperationContract]
        List<Telefono> ObtenerTelefonosPorPersona(string personaID);

        [OperationContract]
        void GuardarPersona(Persona persona);

        [OperationContract]
        void EliminarPersona(string personaID);
    }
}

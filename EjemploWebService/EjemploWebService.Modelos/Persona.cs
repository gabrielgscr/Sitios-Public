using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EjemploWebService.Modelos
{
    [DataContract]
    public class Persona
    {
        [DataMember(Order = 1)]
        public string PersonaID { get; set; }

        [DataMember(Order = 2)]
        public string Nombre { get; set; }

        [DataMember(Order = 3)]
        public byte Tipo { get; set; }

        [DataMember(Order = 4)]
        public string Gender { get; set; }

        [DataMember(Order = 5)]
        public string Password { get; set; }

        [DataMember(Order = 6)]
        public List<Rol> Roles { get; set; } = new List<Rol>();

        [DataMember(Order = 7)]
        public List<Telefono> Telefonos { get; set; } = new List<Telefono>();
    }
}
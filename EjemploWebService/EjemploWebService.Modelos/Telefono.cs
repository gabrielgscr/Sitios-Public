using System.Runtime.Serialization;

namespace EjemploWebService.Modelos
{
    [DataContract]
    public class Telefono
    {
        [DataMember(Order = 1)]
        public int TelefonoID { get; set; }

        [DataMember(Order = 2)]
        public string PersonaID { get; set; }

        [DataMember(Order = 3)]
        public string NumeroTelefono { get; set; }
    }
}
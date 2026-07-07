using System.Runtime.Serialization;

namespace EjemploWebService.Modelos
{
    [DataContract]
    public class Rol
    {
        [DataMember(Order = 1)]
        public int RolID { get; set; }

        [DataMember(Order = 2)]
        public string Nombre { get; set; }
    }
}
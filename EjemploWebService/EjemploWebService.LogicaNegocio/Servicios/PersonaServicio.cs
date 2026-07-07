using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EjemploWebService.AccesoDatos;
using EjemploWebService.Modelos;

namespace EjemploWebService.LogicaNegocio
{
    public class PersonaServicio : IPersonaServicio
    {
        private readonly IPersonaRepositorio _personaRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly ITelefonoRepositorio _telefonoRepositorio;

        public PersonaServicio(string connectionString)
            : this(new PersonaRepositorio(connectionString), new RolRepositorio(connectionString), new TelefonoRepositorio(connectionString))
        {
        }

        internal PersonaServicio(IPersonaRepositorio personaRepositorio, IRolRepositorio rolRepositorio, ITelefonoRepositorio telefonoRepositorio)
        {
            _personaRepositorio = personaRepositorio ?? throw new ArgumentNullException(nameof(personaRepositorio));
            _rolRepositorio = rolRepositorio ?? throw new ArgumentNullException(nameof(rolRepositorio));
            _telefonoRepositorio = telefonoRepositorio ?? throw new ArgumentNullException(nameof(telefonoRepositorio));
        }

        public List<Persona> ObtenerPersonas()
        {
            var personas = _personaRepositorio.ObtenerTodas();
            foreach (var persona in personas)
            {
                CompletarDetalle(persona);
            }

            return personas;
        }

        public Persona ObtenerPersona(string personaID)
        {
            var persona = _personaRepositorio.ObtenerPorId(personaID);
            if (persona != null)
            {
                CompletarDetalle(persona);
            }

            return persona;
        }

        public List<Rol> ObtenerRoles()
        {
            return _rolRepositorio.ObtenerTodos();
        }

        public List<Telefono> ObtenerTelefonosPorPersona(string personaID)
        {
            return _telefonoRepositorio.ObtenerPorPersona(personaID);
        }

        public void GuardarPersona(Persona persona)
        {
            ValidarPersona(persona);
            NormalizarPersona(persona);
            persona.Password = HashearContraseña(persona.Password);

            if (_personaRepositorio.Existe(persona.PersonaID))
            {
                _personaRepositorio.Actualizar(persona);
                return;
            }

            _personaRepositorio.Insertar(persona);
        }

        public void EliminarPersona(string personaID)
        {
            if (string.IsNullOrWhiteSpace(personaID))
            {
                throw new ArgumentException("El identificador de la persona es obligatorio.", nameof(personaID));
            }

            _personaRepositorio.Eliminar(personaID.Trim());
        }

        private void CompletarDetalle(Persona persona)
        {
            persona.Roles = _rolRepositorio.ObtenerPorPersona(persona.PersonaID);
            persona.Telefonos = _telefonoRepositorio.ObtenerPorPersona(persona.PersonaID);
        }

        private static void ValidarPersona(Persona persona)
        {
            if (persona == null)
            {
                throw new ArgumentNullException(nameof(persona));
            }

            if (string.IsNullOrWhiteSpace(persona.PersonaID))
            {
                throw new ArgumentException("La persona debe tener un identificador.", nameof(persona));
            }

            if (string.IsNullOrWhiteSpace(persona.Nombre))
            {
                throw new ArgumentException("El nombre de la persona es obligatorio.", nameof(persona));
            }

            if (string.IsNullOrWhiteSpace(persona.Gender))
            {
                throw new ArgumentException("El género de la persona es obligatorio.", nameof(persona));
            }

            if (string.IsNullOrWhiteSpace(persona.Password))
            {
                throw new ArgumentException("La contraseña de la persona es obligatoria.", nameof(persona));
            }
        }

        private static void NormalizarPersona(Persona persona)
        {
            persona.PersonaID = persona.PersonaID.Trim();
            persona.Nombre = persona.Nombre.Trim();
            persona.Gender = persona.Gender.Trim();

            if (persona.Roles == null)
            {
                persona.Roles = new List<Rol>();
            }

            if (persona.Telefonos == null)
            {
                persona.Telefonos = new List<Telefono>();
            }
        }

        private static string HashearContraseña(string valor)
        {
            using (var algoritmo = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(valor.Trim());
                var hash = algoritmo.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
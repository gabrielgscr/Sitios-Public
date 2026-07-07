using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using EjemploWebService.Modelos;
using EjemploWebService.PruebasWeb.Servicios;

namespace EjemploWebService.PruebasWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtServicioUrl.Text = ConfigurationManager.AppSettings["ServicioPersonasUrl"] ?? string.Empty;
                MostrarMensaje("Ingresa una URL válida del servicio y usa los botones para probar las operaciones.", false);
            }
        }

        protected void btnCargarPersonas_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(() =>
            {
                var personas = ServicioPersonasCliente.ObtenerPersonas(UrlServicio);
                gvPersonas.DataSource = personas.Select(persona => new
                {
                    persona.PersonaID,
                    persona.Nombre,
                    persona.Tipo,
                    persona.Gender,
                    Roles = persona.Roles == null ? 0 : persona.Roles.Count,
                    Telefonos = persona.Telefonos == null ? 0 : persona.Telefonos.Count
                }).ToList();
                gvPersonas.DataBind();
                MostrarMensaje($"Se cargaron {personas.Count} personas.", false);
            });
        }

        protected void btnCargarRoles_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(() =>
            {
                var roles = ServicioPersonasCliente.ObtenerRoles(UrlServicio);
                gvRoles.DataSource = roles;
                gvRoles.DataBind();
                MostrarMensaje($"Se cargaron {roles.Count} roles.", false);
            });
        }

        protected void btnCargarTelefonos_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(() =>
            {
                var personaId = ObtenerPersonaId();
                var telefonos = ServicioPersonasCliente.ObtenerTelefonosPorPersona(UrlServicio, personaId);
                gvTelefonos.DataSource = telefonos;
                gvTelefonos.DataBind();
                MostrarMensaje($"Se cargaron {telefonos.Count} teléfonos para {personaId}.", false);
            });
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(() =>
            {
                var persona = ServicioPersonasCliente.ObtenerPersona(UrlServicio, ObtenerPersonaId());
                if (persona == null)
                {
                    gvTelefonos.DataSource = null;
                    gvTelefonos.DataBind();
                    MostrarMensaje("No se encontró la persona.", true);
                    return;
                }

                CargarPersonaEnFormulario(persona);
                gvTelefonos.DataSource = persona.Telefonos;
                gvTelefonos.DataBind();
                gvRoles.DataSource = persona.Roles;
                gvRoles.DataBind();
                MostrarMensaje($"Persona {persona.PersonaID} cargada.", false);
            });
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(() =>
            {
                var persona = CrearPersonaDesdeFormularioCompleta();
                ServicioPersonasCliente.GuardarPersona(UrlServicio, persona);
                MostrarMensaje($"La persona {persona.PersonaID} se guardó correctamente.", false);
                LimpiarDetalle();
            });
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EjecutarOperacion(() =>
            {
                var personaId = ObtenerPersonaId();
                ServicioPersonasCliente.EliminarPersona(UrlServicio, personaId);
                MostrarMensaje($"La persona {personaId} se eliminó correctamente.", false);
                LimpiarDetalle();
            });
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDetalle();
            MostrarMensaje("Formulario limpio.", false);
        }

        private string UrlServicio => txtServicioUrl.Text.Trim();

        private string ObtenerPersonaId()
        {
            var personaId = txtPersonaId.Text.Trim();
            if (string.IsNullOrWhiteSpace(personaId))
            {
                throw new ArgumentException("Ingresa un PersonaID.", nameof(txtPersonaId));
            }

            return personaId;
        }

        private Persona CrearPersonaDesdeFormularioCompleta()
        {
            var persona = new Persona
            {
                PersonaID = txtPersonaId.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Gender = txtGender.Text.Trim(),
                Password = txtPassword.Text
            };

            if (!byte.TryParse(txtTipo.Text.Trim(), out var tipo))
            {
                throw new ArgumentException("Tipo debe ser un número entre 0 y 255.", nameof(txtTipo));
            }

            persona.Tipo = tipo;
            return persona;
        }

        private void CargarPersonaEnFormulario(Persona persona)
        {
            txtPersonaId.Text = persona.PersonaID;
            txtNombre.Text = persona.Nombre;
            txtTipo.Text = persona.Tipo.ToString();
            txtGender.Text = persona.Gender;
            txtPassword.Text = string.Empty;
        }

        private void LimpiarDetalle()
        {
            txtPersonaId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtTipo.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtPassword.Text = string.Empty;
            gvPersonas.DataSource = null;
            gvPersonas.DataBind();
            gvRoles.DataSource = null;
            gvRoles.DataBind();
            gvTelefonos.DataSource = null;
            gvTelefonos.DataBind();
        }

        private void MostrarMensaje(string mensaje, bool error)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = error ? "mensaje error" : "mensaje ok";
        }

        private void EjecutarOperacion(Action accion)
        {
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
    }
}

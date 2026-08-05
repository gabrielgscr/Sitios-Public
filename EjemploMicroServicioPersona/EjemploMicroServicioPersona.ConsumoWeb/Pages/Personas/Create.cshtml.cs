using EjemploMicroServicioPersona.ConsumoWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace EjemploMicroServicioPersona.ConsumoWeb.Pages.Personas
{
    public class CreateModel : PageModel
    {
        private readonly IPersonaApiClient _api;

        public CreateModel(IPersonaApiClient api)
        {
            _api = api;
        }

        [BindProperty]
        public PersonaDto Persona { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var (ok, status, message) = await _api.CreateAsync(Persona);
                if (!ok)
                {
                    if (status == HttpStatusCode.Conflict)
                    {
                        ErrorMessage = message ?? $"Ya existe una persona con el ID '{Persona.PersonaId}'.";
                    }
                    else
                    {
                        ErrorMessage = message ?? "No se pudo crear la persona";
                    }
                    return Page();
                }
                return RedirectToPage("/Personas/Index");
            }
            catch (HttpRequestException httpEx)
            {
                ErrorMessage = $"Error de red: {httpEx.Message}";
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al crear: {ex.Message}";
                return Page();
            }
        }
    }
}

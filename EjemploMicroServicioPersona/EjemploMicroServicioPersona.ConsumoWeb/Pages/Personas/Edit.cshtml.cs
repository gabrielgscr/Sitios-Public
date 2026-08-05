using EjemploMicroServicioPersona.ConsumoWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjemploMicroServicioPersona.ConsumoWeb.Pages.Personas
{
    public class EditModel : PageModel
    {
        private readonly IPersonaApiClient _api;

        public EditModel(IPersonaApiClient api)
        {
            _api = api;
        }

        [BindProperty]
        public PersonaDto Persona { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var data = await _api.GetByIdAsync(id);
            if (data is null)
            {
                return RedirectToPage("/Personas/Index");
            }
            Persona = data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var ok = await _api.UpdateAsync(Persona);
                if (!ok)
                {
                    ErrorMessage = "No se pudo actualizar la persona";
                    return Page();
                }
                return RedirectToPage("/Personas/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al actualizar: {ex.Message}";
                return Page();
            }
        }
    }
}

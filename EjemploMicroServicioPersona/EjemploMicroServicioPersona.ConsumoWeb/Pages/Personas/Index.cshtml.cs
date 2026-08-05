using EjemploMicroServicioPersona.ConsumoWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjemploMicroServicioPersona.ConsumoWeb.Pages.Personas
{
    public class IndexModel : PageModel
    {
        private readonly IPersonaApiClient _api;
        public IndexModel(IPersonaApiClient api)
        {
            _api = api;
        }

        public List<PersonaDto> Personas { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGet()
        {
            try
            {
                Personas = await _api.GetAllAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"No se pudo cargar el listado: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            try
            {
                var ok = await _api.DeleteAsync(id);
                if (!ok)
                {
                    ErrorMessage = "No se pudo eliminar la persona";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al eliminar: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}

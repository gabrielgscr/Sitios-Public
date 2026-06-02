using CoreWebSample.Entities;
using CoreWebSample.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWebSample.Pages.Personas
{
    public class EditModel : PageModel
    {
        private readonly IPersonaService _personaService;

        public EditModel(IPersonaService personaService)
        {
            _personaService = personaService;
            Persona = new Persona();
        }

        [BindProperty]
        public Persona Persona { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Persona = await _personaService.GetByIdAsync(id);

            if (Persona == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _personaService.UpdateAsync(Persona);

            return RedirectToPage("Index");
        }
    }
}

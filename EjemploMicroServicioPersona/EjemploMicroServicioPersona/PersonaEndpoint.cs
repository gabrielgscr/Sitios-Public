using EjemploMicroServicioPersona.Entities;
using EjemploMicroServicioPersona.Services;
using Microsoft.AspNetCore.Mvc;

namespace EjemploMicroServicioPersona
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes
                .MapGroup("/api/Persona")
                .WithTags(nameof(Persona))
                .RequireCors("ReactDev");

            group.MapGet("/", async ([FromServices] IPersonaService personaService) =>
            {
                return Results.Ok(await personaService.GetAllAsync());
            })
            .WithName("GetAllPersonas")
            .WithOpenApi();

            group.MapGet("/page", async (
                [FromServices] IPersonaService personaService,
                int pageNumber = 1,
                int pageSize = 10) =>
            {
                if (pageNumber < 1)
                {
                    return Results.BadRequest(new { message = "El número de página debe ser mayor que cero." });
                }

                if (pageSize is < 1 or > 100)
                {
                    return Results.BadRequest(new { message = "El tamaño de página debe estar entre 1 y 100." });
                }

                return Results.Ok(await personaService.GetPageAsync(pageNumber, pageSize));
            })
            .WithName("GetPersonasPage")
            .WithOpenApi();

            group.MapGet("/{id}", async ([FromServices] IPersonaService personaService, string id) =>
            {
                var p = await personaService.GetByIdAsync(id);
                return p is null ? Results.NotFound() : Results.Ok(p);
            })
            .WithName("GetPersonaById")
            .WithOpenApi();

            group.MapPost("/", async ([FromServices] IPersonaService personaService, [FromBody] Persona persona) =>
            {
                // 409 si el recurso ya existe
                var exists = await personaService.GetByIdAsync(persona.PersonaId);
                if (exists is not null)
                {
                    return Results.Conflict(new { message = $"Ya existe una persona con el ID '{persona.PersonaId}'." });
                }

                var rows = await personaService.CreateAsync(persona);
                if (rows <= 0)
                {
                    return Results.Problem("No se pudo crear la persona");
                }

                // REST: 201 + Location + representación del recurso creado
                var created = await personaService.GetByIdAsync(persona.PersonaId) ?? persona;
                return Results.Created($"/api/Persona/{created.PersonaId}", created);
            })
            .WithName("CreatePersona")
            .WithOpenApi();

            group.MapPut("/{id}", async ([FromServices] IPersonaService personaService, string id, [FromBody] Persona persona) =>
            {
                if (!string.Equals(id, persona.PersonaId, StringComparison.OrdinalIgnoreCase))
                {
                    return Results.BadRequest(new { message = "El ID de la ruta y el del cuerpo no coinciden" });
                }

                // REST: PUT es idempotente; si no existe, 404
                var exists = await personaService.GetByIdAsync(id);
                if (exists is null)
                {
                    return Results.NotFound();
                }

                var updated = await personaService.UpdateAsync(persona);
                if (updated <= 0)
                {
                    return Results.Problem("No se pudo actualizar la persona");
                }

                var current = await personaService.GetByIdAsync(id) ?? persona;
                return Results.Ok(current);
            })
            .WithName("UpdatePersona")
            .WithOpenApi();

            group.MapDelete("/{id}", async ([FromServices] IPersonaService personaService, string id) =>
            {
                var exists = await personaService.GetByIdAsync(id);
                if (exists is null)
                {
                    return Results.NotFound();
                }

                var deleted = await personaService.DeleteAsync(id);
                return deleted > 0 ? Results.NoContent() : Results.Problem("No se pudo eliminar la persona");
            })
            .WithName("DeletePersona")
            .WithOpenApi();
        }
    }
}

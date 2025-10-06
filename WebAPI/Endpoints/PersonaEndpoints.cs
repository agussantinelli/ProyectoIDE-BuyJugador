using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace WebAPI.Endpoints
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this WebApplication app)
        {
            app.MapGet("/api/personas", async (BuyJugadorContext db) =>
            {
                var personas = await db.Personas
                    .Include(p => p.IdLocalidadNavigation)
                        .ThenInclude(l => l.IdProvinciaNavigation)
                    .Select(p => PersonaDTO.FromDominio(p))
                    .ToListAsync();
                return Results.Ok(personas);
            });

            app.MapGet("/api/personas/inactivos", async (BuyJugadorContext db) =>
            {
                var personas = await db.Personas
                    .IgnoreQueryFilters() 
                    .Where(p => !p.Estado)
                    .Include(p => p.IdLocalidadNavigation)
                        .ThenInclude(l => l.IdProvinciaNavigation)
                    .Select(p => PersonaDTO.FromDominio(p))
                    .ToListAsync();
                return Results.Ok(personas);
            });

            app.MapGet("/api/personas/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas
                    .IgnoreQueryFilters()
                    .Include(p => p.IdLocalidadNavigation)
                        .ThenInclude(l => l.IdProvinciaNavigation)
                    .FirstOrDefaultAsync(p => p.IdPersona == id);

                if (persona == null) return Results.NotFound();
                return Results.Ok(PersonaDTO.FromDominio(persona));
            });

            app.MapPost("/api/personas", async (BuyJugadorContext db, PersonaDTO personaDto) =>
            {
                var persona = new Persona
                {
                    NombreCompleto = personaDto.NombreCompleto,
                    Dni = personaDto.Dni,
                    Email = personaDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(personaDto.Password),
                    Telefono = personaDto.Telefono,
                    Direccion = personaDto.Direccion,
                    IdLocalidad = personaDto.IdLocalidad,
                    FechaIngreso = personaDto.FechaIngreso,
                    Estado = true 
                };
                db.Personas.Add(persona);
                await db.SaveChangesAsync();

                var dtoCreado = PersonaDTO.FromDominio(persona);
                dtoCreado.Password = null;
                return Results.Created($"/api/personas/{persona.IdPersona}", dtoCreado);
            });

            app.MapPut("/api/personas/{id}", async (BuyJugadorContext db, int id, PersonaDTO personaDto) =>
            {
                var persona = await db.Personas.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.IdPersona == id);
                if (persona == null) return Results.NotFound();

                persona.Email = personaDto.Email;
                persona.Telefono = personaDto.Telefono;
                persona.Direccion = personaDto.Direccion;
                persona.IdLocalidad = personaDto.IdLocalidad;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapPut("/api/personas/reactivar/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.IdPersona == id);
                if (persona == null) return Results.NotFound();

                persona.Estado = true;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/api/personas/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas.FindAsync(id);
                if (persona == null) return Results.NotFound();

                persona.Estado = false;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapPost("/api/personas/login", async (BuyJugadorContext db, LoginRequestDto loginRequest) =>
            {
                var persona = await db.Personas
                                      .IgnoreQueryFilters() 
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(p => p.Dni == loginRequest.Dni);

                if (persona == null || !persona.Estado || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, persona.Password))
                {
                    return Results.Unauthorized();
                }

                var personaDto = PersonaDTO.FromDominio(persona);
                personaDto.Password = null;

                return Results.Ok(personaDto);
            }).AllowAnonymous();
        }

        public class LoginRequestDto
        {
            public int Dni { get; set; }
            public string Password { get; set; } = null!;
        }
    }
}


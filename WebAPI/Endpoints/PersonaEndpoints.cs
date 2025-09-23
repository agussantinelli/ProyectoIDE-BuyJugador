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
            // Endpoint para obtener todas las personas ACTIVAS 
            app.MapGet("/api/personas", async (BuyJugadorContext db) =>
            {
                var personas = await db.Personas
                    .Include(p => p.IdLocalidadNavigation)
                        .ThenInclude(l => l.IdProvinciaNavigation)
                    .Select(p => PersonaDTO.FromDominio(p))
                    .ToListAsync();
                return Results.Ok(personas);
            });

            // Endpoint para obtener todas las personas INACTIVAS
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

            // Obtiene una persona por su ID 
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

            // Crea una nueva persona
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
                    Estado = true // Nuevas personas siempre activas
                };
                db.Personas.Add(persona);
                await db.SaveChangesAsync();

                var dtoCreado = PersonaDTO.FromDominio(persona);
                dtoCreado.Password = null;
                return Results.Created($"/api/personas/{persona.IdPersona}", dtoCreado);
            });

            // Actualiza una persona
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

            // Reactiva una persona (cambia estado a true)
            app.MapPut("/api/personas/reactivar/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.IdPersona == id);
                if (persona == null) return Results.NotFound();

                persona.Estado = true;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Borrado lógico de una persona (cambia estado a false)
            app.MapDelete("/api/personas/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas.FindAsync(id);
                if (persona == null) return Results.NotFound();

                persona.Estado = false;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // Endpoint de Login
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


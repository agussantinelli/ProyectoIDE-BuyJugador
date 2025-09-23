using Data;
using DominioModelo;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Endpoints
{
    public static class PersonaEndpoints
    {
        public static void MapPersonaEndpoints(this WebApplication app)
        {
            app.MapGet("/api/personas", async (BuyJugadorContext db) =>
            {
                var personas = await db.Personas.Select(p => new PersonaDTO
                {
                    IdPersona = p.IdPersona,
                    NombreCompleto = p.NombreCompleto,
                    Dni = p.Dni,
                    Email = p.Email,
                    Telefono = p.Telefono,
                    Direccion = p.Direccion,
                    IdLocalidad = p.IdLocalidad,
                    FechaIngreso = p.FechaIngreso
                }).ToListAsync();
                return Results.Ok(personas);
            });

            app.MapGet("/api/personas/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas.FindAsync(id);
                if (persona == null) return Results.NotFound();
                var personaDto = new PersonaDTO
                {
                    IdPersona = persona.IdPersona,
                    NombreCompleto = persona.NombreCompleto,
                    Dni = persona.Dni,
                    Email = persona.Email,
                    Telefono = persona.Telefono,
                    Direccion = persona.Direccion,
                    IdLocalidad = persona.IdLocalidad,
                    FechaIngreso = persona.FechaIngreso
                };
                return Results.Ok(personaDto);
            });

            app.MapPost("/api/personas", async (BuyJugadorContext db, PersonaDTO personaDto) =>
            {
                var persona = new Persona
                {
                    NombreCompleto = personaDto.NombreCompleto,
                    Dni = personaDto.Dni,
                    Email = personaDto.Email,
                    // Directamente asignamos el string del hash
                    Password = BCrypt.Net.BCrypt.HashPassword(personaDto.Password),
                    Telefono = personaDto.Telefono,
                    Direccion = personaDto.Direccion,
                    IdLocalidad = personaDto.IdLocalidad,
                    FechaIngreso = personaDto.FechaIngreso
                };
                db.Personas.Add(persona);
                await db.SaveChangesAsync();

                // Devolvemos un DTO sin la contraseña por seguridad
                personaDto.IdPersona = persona.IdPersona;
                personaDto.Password = null;
                return Results.Created($"/api/personas/{persona.IdPersona}", personaDto);
            });

            app.MapPut("/api/personas/{id}", async (BuyJugadorContext db, int id, PersonaDTO personaDto) =>
            {
                var persona = await db.Personas.FindAsync(id);
                if (persona == null) return Results.NotFound();

                persona.NombreCompleto = personaDto.NombreCompleto;
                // ... (asignar otras propiedades) ...

                if (!string.IsNullOrEmpty(personaDto.Password))
                {
                    // Directamente asignamos el nuevo hash
                    persona.Password = BCrypt.Net.BCrypt.HashPassword(personaDto.Password);
                }

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/api/personas/{id}", async (BuyJugadorContext db, int id) =>
            {
                var persona = await db.Personas.FindAsync(id);
                if (persona == null) return Results.NotFound();

                db.Personas.Remove(persona);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            // --- NUEVO ENDPOINT DE LOGIN ---
            app.MapPost("/api/personas/login", async (BuyJugadorContext db, LoginRequestDto loginRequest) =>
            {
                var persona = await db.Personas
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(p => p.Dni == loginRequest.Dni);

                if (persona == null)
                {
                    return Results.Unauthorized();
                }

                // ¡Mucho más simple! Comparamos el string del formulario con el hash de la BD
                bool passwordValida = BCrypt.Net.BCrypt.Verify(loginRequest.Password, persona.Password);

                if (!passwordValida)
                {
                    return Results.Unauthorized();
                }

                var personaDto = new PersonaDTO
                {
                    IdPersona = persona.IdPersona,
                    NombreCompleto = persona.NombreCompleto,
                    Email = persona.Email,
                    FechaIngreso = persona.FechaIngreso
                };

                return Results.Ok(personaDto);

            }).AllowAnonymous();
        }

        // DTO para el cuerpo de la solicitud de login
        public class LoginRequestDto
        {
            public int Dni { get; set; }
            public string Password { get; set; }
        }
    }
}

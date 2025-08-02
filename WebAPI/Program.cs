using DominioServicios;
using DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using DominioModelo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });

// Registrar los servicios de dominio como Singletons
builder.Services.AddSingleton<ProvinciaService>();
builder.Services.AddSingleton<TipoProductoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

#region Provincias
// Sección dedicada a los endpoints de la entidad Provincia

// Endpoint para obtener todas las provincias
app.MapGet("/provincias", (ProvinciaService provinciaService) =>
{
    var provinciasDominio = provinciaService.GetAll();
    // Mapea la lista de DominioModelo.Provincia a una lista de DTOs.Provincia
    var provinciasDto = provinciasDominio.Select(p => new DTOs.Provincia { CodigoProvincia = p.CodigoProvincia, NombreProvincia = p.NombreProvincia }).ToList();
    return Results.Ok(provinciasDto);
})
.WithName("GetAllProvincias")
.Produces<IReadOnlyList<DTOs.Provincia>>(StatusCodes.Status200OK)
.WithOpenApi();

// Endpoint para obtener una provincia por su código
app.MapGet("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
{
    var provinciaDominio = provinciaService.Get(codigoProvincia);
    if (provinciaDominio is null)
    {
        return Results.NotFound();
    }
    var provinciaDto = new DTOs.Provincia { CodigoProvincia = provinciaDominio.CodigoProvincia, NombreProvincia = provinciaDominio.NombreProvincia };
    return Results.Ok(provinciaDto);
})
.WithName("GetProvinciaByCodigo")
.Produces<DTOs.Provincia>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// Endpoint para agregar una nueva provincia
app.MapPost("/provincias", (DTOs.Provincia dto, ProvinciaService provinciaService) =>
{
    try
    {
        // Se crea la instancia de DominioModelo.Provincia usando el constructor con los datos del DTO
        var provincia = new DominioModelo.Provincia(dto.CodigoProvincia, dto.NombreProvincia);
        bool added = provinciaService.Add(provincia);
        return added ? Results.Created($"/provincias/{provincia.CodigoProvincia}", dto) : Results.Conflict(new { error = "Ya existe una provincia con ese nombre o código." });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddProvincia")
.Produces<DTOs.Provincia>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status409Conflict)
.WithOpenApi();

// Endpoint para actualizar una provincia existente
app.MapPut("/provincias", (DTOs.Provincia dto, ProvinciaService provinciaService) =>
{
    try
    {
        // Se crea la instancia de DominioModelo.Provincia usando el constructor con los datos del DTO
        var provincia = new DominioModelo.Provincia(dto.CodigoProvincia, dto.NombreProvincia);
        bool updated = provinciaService.Update(provincia);
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateProvincia")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

// Endpoint para eliminar una provincia por su código
app.MapDelete("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
{
    bool deleted = provinciaService.Delete(codigoProvincia);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteProvincia")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

#endregion

#region TipoProducto
// Sección dedicada a los endpoints de la entidad TipoProducto

// Endpoint para obtener todos los tipos de producto
app.MapGet("/tiposproducto", (TipoProductoService tipoProductoService) =>
{
    var tiposProductoDominio = tipoProductoService.GetAll();
    // Usando el constructor del DTO para crear la lista
    var tiposProducto = tiposProductoDominio.Select(tp => new DominioModelo.TipoProducto(tp.IdTipoProducto, tp.NombreTipoProducto)).ToList();
    return Results.Ok(tiposProducto);
})
.WithName("GetAllTiposProducto")
.Produces<IReadOnlyList<DTOs.TipoProducto>>(StatusCodes.Status200OK)
.WithOpenApi();

// Endpoint para obtener un tipo de producto por su ID
app.MapGet("/tiposproducto/{id}", (int id, TipoProductoService tipoProductoService) =>
{
    var tipoProductoDominio = tipoProductoService.Get(id);
    if (tipoProductoDominio is null)
    {
        return Results.NotFound();
    }
    // Usando el constructor del DTO para crear la instancia
    var tipoProducto = new DominioModelo.TipoProducto(tipoProductoDominio.IdTipoProducto, tipoProductoDominio.NombreTipoProducto);
    return Results.Ok(tipoProducto);
})
.WithName("GetTipoProductoById")
.Produces<DTOs.TipoProducto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// Endpoint para agregar un nuevo tipo de producto
app.MapPost("/tiposproducto", (DTOs.TipoProducto dto, TipoProductoService tipoProductoService) =>
{
    try
    {
        // Se crea la instancia de DominioModelo.TipoProducto usando el constructor con los datos del DTO
        var tipoProducto = new DominioModelo.TipoProducto(dto.IdTipoProducto, dto.NombreTipoProducto);
        bool added = tipoProductoService.Add(tipoProducto);
        return added ? Results.Created($"/tiposproducto/{tipoProducto.IdTipoProducto}", dto) : Results.Conflict(new { error = "Ya existe un tipo de producto con ese nombre." });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddTipoProducto")
.Produces<DTOs.TipoProducto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status409Conflict)
.WithOpenApi();

// Endpoint para actualizar un tipo de producto existente
app.MapPut("/tiposproducto", (DTOs.TipoProducto dto, TipoProductoService tipoProductoService) =>
{
    try
    {
        // Se crea la instancia de DominioModelo.TipoProducto usando el constructor con los datos del DTO
        var tipoProducto = new DominioModelo.TipoProducto(dto.IdTipoProducto, dto.NombreTipoProducto);
        bool updated = tipoProductoService.Update(tipoProducto);
        return updated ? Results.NoContent() : Results.NotFound();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateTipoProducto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest)
.WithOpenApi();

// Endpoint para eliminar un tipo de producto por su ID
app.MapDelete("/tiposproducto/{id}", (int id, TipoProductoService tipoProductoService) =>
{
    bool deleted = tipoProductoService.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteTipoProducto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

#endregion

app.Run();
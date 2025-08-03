using Domain.Services;
using DominioModelo;
using DominioServicio;
using DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ProvinciaService>();
builder.Services.AddSingleton<TipoProductoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- Endpoints para Provincias ---
#region Provincias
app.MapGet("/provincias", (ProvinciaService provinciaService) =>
{
    var provincias = provinciaService.GetAll();
    var dtos = provincias.Select(p => new ProvinciaDto
    {
        CodigoProvincia = p.CodigoProvincia,
        NombreProvincia = p.NombreProvincia
    }).ToList();
    return Results.Ok(dtos);
})
.WithName("GetAllProvincias")
.Produces<List<ProvinciaDto>>(StatusCodes.Status200OK);

app.MapGet("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
{
    var provincia = provinciaService.Get(codigoProvincia);
    if (provincia == null)
    {
        return Results.NotFound("Provincia no encontrada.");
    }
    var dto = new ProvinciaDto
    {
        CodigoProvincia = provincia.CodigoProvincia,
        NombreProvincia = provincia.NombreProvincia
    };
    return Results.Ok(dto);
})
.WithName("GetProvinciaByCodigo")
.Produces<ProvinciaDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/provincias", (ProvinciaDto dto, ProvinciaService provinciaService) =>
{
    try
    {
        var nuevaProvincia = new Provincia(dto.CodigoProvincia, dto.NombreProvincia);
        provinciaService.Add(nuevaProvincia);
        dto.CodigoProvincia = nuevaProvincia.CodigoProvincia; // Actualizar el DTO con el nuevo ID generado
        return Results.Created($"/provincias/{dto.CodigoProvincia}", dto);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddProvincia")
.Produces<ProvinciaDto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

app.MapPut("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaDto dto, ProvinciaService provinciaService) =>
{
    try
    {
        dto.CodigoProvincia = codigoProvincia;
        var provinciaActualizada = new Provincia(dto.CodigoProvincia, dto.NombreProvincia);
        var found = provinciaService.Update(provinciaActualizada);
        if (!found)
        {
            return Results.NotFound("Provincia no encontrada.");
        }
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateProvincia")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest);

app.MapDelete("/provincias/{codigoProvincia}", (int codigoProvincia, ProvinciaService provinciaService) =>
{
    var deleted = provinciaService.Delete(codigoProvincia);
    if (!deleted)
    {
        return Results.NotFound("Provincia no encontrada.");
    }
    return Results.NoContent();
})
.WithName("DeleteProvincia")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);
#endregion

// --- Endpoints para Tipos de Producto ---
#region Tipos de Productos
app.MapGet("/tiposproducto", (TipoProductoService tipoProductoService) =>
{
    var tiposProducto = tipoProductoService.GetAll();
    var dtos = tiposProducto.Select(tp => new TipoProductoDto
    {
        IdTipoProducto = tp.IdTipoProducto,
        NombreTipoProducto = tp.NombreTipoProducto
    }).ToList();
    return Results.Ok(dtos);
})
.WithName("GetAllTiposProducto")
.Produces<List<TipoProductoDto>>(StatusCodes.Status200OK);

app.MapGet("/tiposproducto/{idTipoProducto}", (int idTipoProducto, TipoProductoService tipoProductoService) =>
{
    var tipoProducto = tipoProductoService.Get(idTipoProducto);
    if (tipoProducto == null)
    {
        return Results.NotFound("Tipo de producto no encontrado.");
    }
    var dto = new TipoProductoDto
    {
        IdTipoProducto = tipoProducto.IdTipoProducto,
        NombreTipoProducto = tipoProducto.NombreTipoProducto
    };
    return Results.Ok(dto);
})
.WithName("GetTipoProductoById")
.Produces<TipoProductoDto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);

app.MapPost("/tiposproducto", (TipoProductoDto dto, TipoProductoService tipoProductoService) =>
{
    try
    {
        var nuevoTipoProducto = new TipoProducto(dto.IdTipoProducto, dto.NombreTipoProducto);
        tipoProductoService.Add(nuevoTipoProducto);
        dto.IdTipoProducto = nuevoTipoProducto.IdTipoProducto; // Actualizar el DTO con el nuevo ID generado
        return Results.Created($"/tiposproducto/{dto.IdTipoProducto}", dto);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("AddTipoProducto")
.Produces<TipoProductoDto>(StatusCodes.Status201Created)
.Produces(StatusCodes.Status400BadRequest);

app.MapPut("/tiposproducto/{idTipoProducto}", (int idTipoProducto, TipoProductoDto dto, TipoProductoService tipoProductoService) =>
{
    try
    {
        dto.IdTipoProducto = idTipoProducto;
        var tipoProductoActualizado = new TipoProducto(dto.IdTipoProducto, dto.NombreTipoProducto);
        var found = tipoProductoService.Update(tipoProductoActualizado);
        if (!found)
        {
            return Results.NotFound("Tipo de producto no encontrado.");
        }
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("UpdateTipoProducto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status400BadRequest);

app.MapDelete("/tiposproducto/{idTipoProducto}", (int idTipoProducto, TipoProductoService tipoProductoService) =>
{
    var deleted = tipoProductoService.Delete(idTipoProducto);
    if (!deleted)
    {
        return Results.NotFound("Tipo de producto no encontrado.");
    }
    return Results.NoContent();
})
.WithName("DeleteTipoProducto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound);
#endregion

app.Run();

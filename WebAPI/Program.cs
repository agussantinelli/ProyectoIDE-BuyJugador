using DominioServicios;
using DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using DominioModelo;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services for OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(o => { });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// Endpoints para Provincias
#region Provincias


app.MapGet("/provincias", ([FromServices] DominioServicios.ProvinciaService service) =>
{
    return Results.Ok(service.GetAll());
});

app.MapPost("/provincias", ([FromServices] DominioServicios.ProvinciaService service, [FromBody] DTOs.Provincia provinciaDto) =>
{
    if (string.IsNullOrEmpty(provinciaDto.NombreProvincia))
    {
        return Results.BadRequest("El nombre de la provincia es obligatorio.");
    }

    var provincia = new DominioModelo.Provincia(provinciaDto.CodigoProvincia, provinciaDto.NombreProvincia);
    if (service.Add(provincia))
    {
        return Results.Created($"/provincias/{provincia.CodigoProvincia}", provincia);
    }
    return Results.Conflict("Ya existe una provincia con este código.");
});

app.MapPut("/provincias/{codigo:int}", ([FromRoute] int codigo, [FromServices] DominioServicios.ProvinciaService service, [FromBody] DTOs.Provincia provinciaDto) =>
{
    if (string.IsNullOrEmpty(provinciaDto.NombreProvincia))
    {
        return Results.BadRequest("El nombre de la provincia es obligatorio.");
    }

    var provinciaActualizada = new DominioModelo.Provincia(codigo, provinciaDto.NombreProvincia);

    if (service.Update(codigo, provinciaActualizada))
    {
        return Results.Ok(provinciaActualizada);
    }
    return Results.NotFound($"No se encontró la provincia con el código '{codigo}'.");
});


app.MapDelete("/provincias/{codigo:int}", ([FromRoute] int codigo, [FromServices] DominioServicios.ProvinciaService service) =>
{
    if (service.Delete(codigo))
    {
        return Results.NoContent();
    }
    return Results.NotFound($"No se encontró la provincia con el código '{codigo}'.");
});

#endregion

// Endpoints para Tipos de Producto
#region TipoProducto

app.MapGet("/tiposproducto", ([FromServices] DominioServicios.TipoProductoService service) =>
{
    return Results.Ok(service.GetAll());
});

app.MapPost("/tiposproducto", ([FromServices] DominioServicios.TipoProductoService service, [FromBody] DTOs.TipoProductoDto tipoProductoDto) =>
{
    if (string.IsNullOrEmpty(tipoProductoDto.NombreTipoProducto))
    {
        return Results.BadRequest("El nombre del tipo de producto es obligatorio.");
    }

    var tipoProducto = new DominioModelo.TipoProducto(tipoProductoDto.IdTipoProducto, tipoProductoDto.NombreTipoProducto);
    var nuevoTipo = service.Add(tipoProducto);
    return Results.Created($"/tiposproducto/{nuevoTipo.IdTipoProducto}", nuevoTipo);
});

app.MapPut("/tiposproducto/{id:int}", ([FromRoute] int id, [FromServices] DominioServicios.TipoProductoService service, [FromBody] DTOs.TipoProductoDto tipoProductoDto) =>
{
    if (string.IsNullOrEmpty(tipoProductoDto.NombreTipoProducto))
    {
        return Results.BadRequest("El nombre del tipo de producto es obligatorio.");
    }

    var tipoProductoActualizado = new DominioModelo.TipoProducto(id, tipoProductoDto.NombreTipoProducto);

    if (service.Update(id, tipoProductoActualizado))
    {
        return Results.Ok(tipoProductoActualizado);
    }
    return Results.NotFound($"No se encontró el tipo de producto con el id '{id}'.");
});

app.MapDelete("/tiposproducto/{id:int}", ([FromRoute] int id, [FromServices] DominioServicios.TipoProductoService service) =>
{
    if (service.Delete(id))
    {
        return Results.NoContent();
    }
    return Results.NotFound($"No se encontró el tipo de producto con el id '{id}'.");
});
#endregion

app.Run();
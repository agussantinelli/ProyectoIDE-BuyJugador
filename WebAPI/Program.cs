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

#region Provincias
// CRUD operations for Provincia entity
// GET all provincias
app.MapGet("/provincias", () =>
{
    ProvinciaService provinciaService = new ProvinciaService();
    var provinciasDominio = provinciaService.GetAll();
    var provinciasDto = provinciasDominio.Select(p => new DTOs.Provincia { CodigoProvincia = p.CodigoProvincia, NombreProvincia = p.NombreProvincia }).ToList();
    return Results.Ok(provinciasDto);
})
.WithName("GetAllProvincias")
.Produces<IReadOnlyList<DTOs.Provincia>>(StatusCodes.Status200OK)
.WithOpenApi();

// GET a provincia by id
app.MapGet("/provincias/{codigoProvincia}", (int codigoProvincia) =>
{
    ProvinciaService provinciaService = new ProvinciaService();
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

// POST a new provincia
app.MapPost("/provincias", (DTOs.Provincia dto) =>
{
    try
    {
        ProvinciaService provinciaService = new ProvinciaService();
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

// PUT to update an existing provincia
app.MapPut("/provincias", (DTOs.Provincia dto) =>
{
    try
    {
        ProvinciaService provinciaService = new ProvinciaService();
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

// DELETE a province
app.MapDelete("/provincias/{codigoProvincia}", (int codigoProvincia) =>
{
    ProvinciaService provinciaService = new ProvinciaService();
    bool deleted = provinciaService.Delete(codigoProvincia);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteProvincia")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

#endregion

#region TipoProducto
// CRUD operations for TipoProducto entity
// GET all product types
app.MapGet("/tiposproducto", () =>
{
    TipoProductoService tipoProductoService = new TipoProductoService();
    var tiposProductoDominio = tipoProductoService.GetAll();
    var tiposProductoDto = tiposProductoDominio.Select(tp => new DominioModelo.TipoProducto(tp.IdTipoProducto, tp.NombreTipoProducto)).ToList();
    return Results.Ok(tiposProductoDto);
})
.WithName("GetAllTiposProducto")
.Produces<IReadOnlyList<DTOs.TipoProducto>>(StatusCodes.Status200OK)
.WithOpenApi();

// GET a product type by id
app.MapGet("/tiposproducto/{id}", (int id) =>
{
    TipoProductoService tipoProductoService = new TipoProductoService();
    var tipoProductoDominio = tipoProductoService.Get(id);
    if (tipoProductoDominio is null)
    {
        return Results.NotFound();
    }
    var tipoProductoDto = new DominioModelo.TipoProducto(tipoProductoDominio.IdTipoProducto, tipoProductoDominio.NombreTipoProducto);
    return Results.Ok(tipoProductoDto);
})
.WithName("GetTipoProductoById")
.Produces<DTOs.TipoProducto>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

// POST a new product type
app.MapPost("/tiposproducto", (DTOs.TipoProducto dto) =>
{
    try
    {
        TipoProductoService tipoProductoService = new TipoProductoService();
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

// PUT to update an existing product type
app.MapPut("/tiposproducto", (DTOs.TipoProducto dto) =>
{
    try
    {
        TipoProductoService tipoProductoService = new TipoProductoService();
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

// DELETE a product type
app.MapDelete("/tiposproducto/{id}", (int id) =>
{
    TipoProductoService tipoProductoService = new TipoProductoService();
    bool deleted = tipoProductoService.Delete(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteTipoProducto")
.Produces(StatusCodes.Status204NoContent)
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

#endregion

app.Run();
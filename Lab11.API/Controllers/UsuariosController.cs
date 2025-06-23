using Lab11.Application.Reports;
using Lab11.Application.DTOs;
using Lab11.Domain.Entities;
using Lab11.Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public UsuariosController(AppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpPost]
    public IActionResult CrearUsuario([FromBody] UsuarioDto dto)
    {
        var usuario = new Usuario
        {
            Username = dto.Username,
            Password = dto.Password,
            Email = dto.Email,
            FechaCreacion = DateTime.Now
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return Ok("Usuario creado exitosamente.");
    }

    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        var usuarios = _context.Usuarios.ToList();
        return Ok(usuarios);
    }

    [HttpGet("reporte-excel")]
    public async Task<IActionResult> GenerarReporteExcelUsuarios()
    {
        var contenido = await _mediator.Send(new GetUsuariosExcelReportQuery());

        return File(
            fileContents: contenido,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "ReporteUsuarios.xlsx"
        );
    }
}
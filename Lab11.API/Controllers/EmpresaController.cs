using Lab11.Application.Reports;
using Lab11.Application.Commands;
using Lab11.Application.Queries;
using Lab11.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmpresaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddEmpresaCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { Id = id });
    }

    [HttpGet]
    public async Task<ActionResult<List<Empresa>>> Get()
    {
        var result = await _mediator.Send(new GetEmpresasQuery());
        return Ok(result);
    }
    //
    [HttpGet("reporte-excel")]
    public async Task<IActionResult> GenerarReporteExcelEmpresas()
    {
        var contenido = await _mediator.Send(new GetEmpresasExcelReportQuery());

        return File(
            fileContents: contenido,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: "ReporteEmpresas.xlsx"
        );
    }

}
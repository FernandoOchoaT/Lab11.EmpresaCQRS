using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Lab11.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.Reports;

public class GetEmpresasExcelReportHandler : IRequestHandler<GetEmpresasExcelReportQuery, byte[]>
{
    private readonly AppDbContext _context;

    public GetEmpresasExcelReportHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> Handle(GetEmpresasExcelReportQuery request, CancellationToken cancellationToken)
    {
        var empresas = await _context.Empresas.ToListAsync(cancellationToken);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Empresas");

        // Encabezados con estilo
        var headers = new[] { "ID", "Razón Social", "RUC", "Teléfono", "Fecha de Creación" };

        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        // Datos
        for (int i = 0; i < empresas.Count; i++)
        {
            var empresa = empresas[i];
            worksheet.Cell(i + 2, 1).Value = empresa.Id.ToString();
            worksheet.Cell(i + 2, 2).Value = empresa.RazonSocial;
            worksheet.Cell(i + 2, 3).Value = empresa.Ruc;
            worksheet.Cell(i + 2, 4).Value = empresa.Telefono;
            worksheet.Cell(i + 2, 5).Value = empresa.FechaCreacion.ToString("dd/MM/yyyy");

            for (int j = 1; j <= headers.Length; j++)
            {
                worksheet.Cell(i + 2, j).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}

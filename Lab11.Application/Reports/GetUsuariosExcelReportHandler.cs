using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Lab11.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.Reports;

public class GetUsuariosExcelReportHandler : IRequestHandler<GetUsuariosExcelReportQuery, byte[]>
{
    private readonly AppDbContext _context;

    public GetUsuariosExcelReportHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> Handle(GetUsuariosExcelReportQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _context.Usuarios.ToListAsync(cancellationToken);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Usuarios");

        // Encabezados con estilo
        var headers = new[] { "ID", "Username", "Email", "Password", "Fecha de Creación" };

        for (int i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.LightGreen;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        }

        // Datos
        for (int i = 0; i < usuarios.Count; i++)
        {
            var usuario = usuarios[i];
            worksheet.Cell(i + 2, 1).Value = usuario.UserId.ToString();
            worksheet.Cell(i + 2, 2).Value = usuario.Username;
            worksheet.Cell(i + 2, 3).Value = usuario.Email;
            worksheet.Cell(i + 2, 4).Value = usuario.Password;
            worksheet.Cell(i + 2, 5).Value = usuario.FechaCreacion.ToString("dd/MM/yyyy");

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

using Lab11.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.Services
{
    public class DataCleanupService
    {
        private readonly AppDbContext _context;

        public DataCleanupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LimpiarEmpresasAntiguas()
        {
            var fechaLimite = DateTime.Now.AddDays(-30);

            var empresasAntiguas = await _context.Empresas
                .Where(e => e.FechaCreacion < fechaLimite)
                .ToListAsync();

            _context.Empresas.RemoveRange(empresasAntiguas);
            await _context.SaveChangesAsync();

            Console.WriteLine($"🧹 {empresasAntiguas.Count} empresas eliminadas el {DateTime.Now}");
        }
    }
}
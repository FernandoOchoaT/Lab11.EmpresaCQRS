using Lab11.Infrastructure.Context;

public class UserCleanupService
{
    private readonly AppDbContext _context;

    public UserCleanupService(AppDbContext context)
    {
        _context = context;
    }

    public void CleanOldUsers()
    {
        var limite = DateTime.Now.AddDays(-30);
        var usuariosAntiguos = _context.Usuarios
            .Where(u => u.FechaCreacion < limite)
            .ToList();

        Console.WriteLine($"{usuariosAntiguos.Count} usuarios eliminados.");

        _context.Usuarios.RemoveRange(usuariosAntiguos);
        _context.SaveChanges();
    }
}
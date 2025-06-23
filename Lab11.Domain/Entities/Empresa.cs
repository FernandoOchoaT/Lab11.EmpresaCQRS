namespace Lab11.Domain.Entities;

public class Empresa
{
    public Guid Id { get; set; }
    public string RazonSocial { get; set; } = null!;
    public string? Ruc { get; set; }
    public string? Telefono { get; set; }
    public DateTime FechaCreacion { get; set; }

}
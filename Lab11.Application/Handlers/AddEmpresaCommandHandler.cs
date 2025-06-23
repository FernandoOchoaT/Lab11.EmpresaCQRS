using Lab11.Application.Commands;
using Lab11.Domain.Entities;
using Lab11.Infrastructure.Context;
using MediatR;

namespace Lab11.Application.Handlers;

public class AddEmpresaCommandHandler : IRequestHandler<AddEmpresaCommand, Guid>
{
    private readonly AppDbContext _context;

    public AddEmpresaCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddEmpresaCommand request, CancellationToken cancellationToken)
    {
        var empresa = new Empresa
        {
            Id = Guid.NewGuid(),
            RazonSocial = request.RazonSocial,
            Ruc = request.Ruc,
            Telefono = request.Telefono
        };

        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync(cancellationToken);
        return empresa.Id;
    }
}
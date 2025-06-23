using Lab11.Application.Queries;
using Lab11.Domain.Entities;
using Lab11.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.Handlers;

public class GetEmpresasQueryHandler : IRequestHandler<GetEmpresasQuery, List<Empresa>>
{
    private readonly AppDbContext _context;

    public GetEmpresasQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Empresa>> Handle(GetEmpresasQuery request, CancellationToken cancellationToken)
    {
        return await _context.Empresas.ToListAsync(cancellationToken);
    }
}
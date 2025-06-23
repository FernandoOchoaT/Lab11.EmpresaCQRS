using Lab11.Domain.Entities;
using MediatR;

namespace Lab11.Application.Commands;

public record AddEmpresaCommand(string RazonSocial, string? Ruc, string? Telefono) : IRequest<Guid>;
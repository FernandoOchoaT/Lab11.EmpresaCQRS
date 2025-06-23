using Lab11.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Lab11.Application.Queries;

public record GetEmpresasQuery() : IRequest<List<Empresa>>;
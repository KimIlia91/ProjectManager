using ErrorOr;
using MediatR;
using PM.Application.Features.RoleContext.Dtos;

namespace PM.Application.Features.RoleContext.Commands.GetRoleList;

/// <summary>
/// Represents a query to retrieve a list of roles.
/// </summary>
public sealed record GetRoleListQuery() : IRequest<ErrorOr<List<GetRoleListResult>>>;

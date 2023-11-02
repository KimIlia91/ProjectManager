using ErrorOr;
using MediatR;
using PM.Application.Features.RoleContext.Dtos;

namespace PM.Application.Features.RoleContext.Commands.GetRoleList;

public sealed record GetRoleListQuery() : IRequest<ErrorOr<List<GetRoleListResult>>>;

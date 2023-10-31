using ErrorOr;
using MediatR;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Common.Enums;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

public sealed record CreateProjectCommand(
    string Name,
    int CustomerCompanyId,
    int ExecutorCompanyId,
    int ManagerId,
    DateTime StartDate,
    DateTime EndDate,
    ProjectPriority Priority) : IRequest<ErrorOr<CreateProjectResult>>;

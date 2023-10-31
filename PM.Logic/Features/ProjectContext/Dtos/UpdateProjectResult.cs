using PM.Domain.Common.Enums;

namespace PM.Application.Features.ProjectContext.Dtos;

public sealed record UpdateProjectResult(
     int Id,
     string Name,
     int CustomerCompanyId,
     int ExecutorCompanyId,
     int ManagerId,
     DateTime StartDate,
     DateTime EndDate,
     ProjectPriority Priority);
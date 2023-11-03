﻿using ErrorOr;
using MediatR;
using PM.Application.Common.Extensions;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Queries.GetManagerProjects;

internal sealed class GetManagerProjectListQueryHandler
    : IRequestHandler<GetManagerProjectListQuery, ErrorOr<List<GetManagerProjectListResult>>>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IProjectRepository _projectRepository;

    public GetManagerProjectListQueryHandler(
        ICurrentUserService currentUser,
        IProjectRepository projectRepository)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<List<GetManagerProjectListResult>>> Handle(
        GetManagerProjectListQuery query, 
        CancellationToken cancellationToken)
    {
        var projectQuery = _projectRepository
            .GetQuiery(asNoTracking: true)
            .Where(p => p.Manager.Id == _currentUser.UserId)
            .Filter(query.Filetr)
            .Sort(query.SotrBy);

        return await _projectRepository
            .ToListResultAsync<GetManagerProjectListResult>(projectQuery, cancellationToken);
    }
}

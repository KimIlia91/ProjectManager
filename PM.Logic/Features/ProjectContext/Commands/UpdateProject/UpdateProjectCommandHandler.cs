using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

/// <summary>
/// Represents a handler for updating a project.
/// </summary>
internal sealed class UpdateProjectCommandHandler
    : IRequestHandler<UpdateProjectCommand, ErrorOr<UpdateProjectResult>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectCommandHandler"/> class.
    /// </summary>
    /// <param name="projectRepository">The repository for accessing project data.</param>
    /// <param name="mapper">The mapper for mapping between data models.</param>
    public UpdateProjectCommandHandler(
        IProjectRepository projectRepository, 
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the update project command.
    /// </summary>
    /// <param name="command">The update project command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="ErrorOr{T}"/> representing the result of the update operation.</returns>
    public async Task<ErrorOr<UpdateProjectResult>> Handle(
        UpdateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        var result = command.Project.Update(
            command.Name,
            command.CustomerCompany,
            command.ExecutorCompany,
            command.Manager,
            command.StartDate,
            command.EndDate,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        await _projectRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateProjectResult>(result.Value);
    }
}

using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;

namespace PM.Application.Features.ProjectContext.Commands.UpdateProject;

public sealed class UpdateProjectCommandHandler
    : IRequestHandler<UpdateProjectCommand, ErrorOr<UpdateProjectResult>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public UpdateProjectCommandHandler(
        IProjectRepository projectRepository, 
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<UpdateProjectResult>> Handle(
        UpdateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        var result = command.Project.Update(
            command.Name,
            command.CustomerCompany,
            command.ExecutorCompany,
            command.ManagerId,
            command.StartDate,
            command.EndDate,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        await _projectRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateProjectResult>(result.Value);
    }
}

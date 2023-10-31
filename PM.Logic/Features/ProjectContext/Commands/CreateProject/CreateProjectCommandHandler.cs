using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.ProjectContext.Dtos;
using PM.Domain.Entities;

namespace PM.Application.Features.ProjectContext.Commands.CreateProject;

public sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, ErrorOr<CreateProjectResult>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CreateProjectResult>> Handle(
        CreateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository
            .GetOrDeafaultAsync(p => p.Name == command.Name, cancellationToken);

        if (project is not null)
            return Error.Conflict("Project already exist", nameof(command.Name));

        var customerCompany = await _companyRepository
            .GetOrDeafaultAsync(c => c.Id == command.CustomerCompanyId, cancellationToken);

        if (customerCompany is null)
            return Error.NotFound("Not found", nameof(command.CustomerCompanyId));

        var executorCompany = await _companyRepository
            .GetOrDeafaultAsync(c => c.Id == command.ExecutorCompanyId, cancellationToken);

        if (executorCompany is null)
            return Error.NotFound("Not found", nameof(command.ExecutorCompanyId));

        var result = Project.Create(
            command.Name, 
            customerCompany, 
            executorCompany, 
            command.ManagerId, 
            command.StartDate, 
            command.EndDate,
            command.Priority);

        if (result.IsError)
            return result.Errors;

        return new CreateProjectResult(result.Value.Id);
    }
}
using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

/// <summary>
/// Handler for updating a task.
/// </summary>
internal sealed class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand, ErrorOr<UpdateTaskResult>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskCommandHandler"/> class.
    /// </summary>
    /// <param name="taskRepository">The task repository.</param>
    /// <param name="mapper">The mapper for mapping entities.</param>
    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Handles the update task command.
    /// </summary>
    /// <param name="command">The update task command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<ErrorOr<UpdateTaskResult>> Handle(
        UpdateTaskCommand command, 
        CancellationToken cancellationToken)
    {
        command.Task!.Update(
            command.Name,
            command.Author,
            command.Executor,
            command.Comment,
            command.Status,
            command.Priority);

        await _taskRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateTaskResult>(command.Task);
    }
}

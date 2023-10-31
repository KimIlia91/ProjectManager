using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Features.TaskContext.Dtos;

namespace PM.Application.Features.TaskContext.Commands.UpdateTask;

internal sealed class UpdateTaskCommandHandler
    : IRequestHandler<UpdateTaskCommand, ErrorOr<UpdateTaskResult>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    public async Task<ErrorOr<UpdateTaskResult>> Handle(
        UpdateTaskCommand command, 
        CancellationToken cancellationToken)
    {
        command.Task!.Update(
            command.Name,
            command.Author!,
            command.Executor!,
            command.Comment!,
            command.Status,
            command.Priority);

        await _taskRepository.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UpdateTaskResult>(command.Task);
    }
}

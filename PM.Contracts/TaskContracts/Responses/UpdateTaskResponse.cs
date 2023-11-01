namespace PM.Contracts.TaskContracts.Responses;

public sealed record UpdateTaskResponse(
    int Id,
    string Name,
    int AuthorId,
    int ExecutorId,
    string? Comment,
    string Status,
    int Priority);

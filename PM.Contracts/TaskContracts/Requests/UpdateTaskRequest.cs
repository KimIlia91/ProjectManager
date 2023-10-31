namespace PM.Contracts.TaskContracts.Requests;

public sealed record UpdateTaskRequest(
    int Id,
    string Name,
    int AuthorId,
    int ExecutorId,
    string? Commnet,
    int Status,
    int Priority);

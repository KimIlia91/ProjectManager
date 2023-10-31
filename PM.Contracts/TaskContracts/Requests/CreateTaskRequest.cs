namespace PM.Contracts.TaskContracts.Requests;

public sealed record CreateTaskRequest(
    int ProjectId,
    string Name,
    int AuthorId,
    int ExecutorId,
    string? Commnet,
    int Status,
    int Priority);

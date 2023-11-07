using ErrorOr;
using MediatR;
using PM.Application.Common.Models.Employee;

namespace PM.Application.Features.UserContext.Queries.GetProjectUserList;

/// <summary>
/// A record representing a query to retrieve a list of users associated with a project.
/// </summary>
/// <param name="ProjectId">Project ID</param>
public sealed record GetUsersOfProjectQuery(
    int ProjectId) : IRequest<ErrorOr<List<UserResult>>>;

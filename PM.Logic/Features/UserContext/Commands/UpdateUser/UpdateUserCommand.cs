using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.UserContext.Commands.UpdateUser;

public sealed class UpdateUserCommand
    : IRequest<ErrorOr<UpdateUserResult>>
{
    [JsonIgnore] public User? Employee { get; set; }

    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddelName { get; set; }

    public string Email { get; set; } = null!;

    public string RoleName { get; set; } = null!;
}
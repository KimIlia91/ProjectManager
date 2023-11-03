using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.UserContext.Commands.DeleteUser;

public sealed class DeleteUserCommand
    : IRequest<ErrorOr<DeleteUserResult>>
{
    [JsonIgnore] public User? Employee { get; set; }

    public int EmployeeId { get; set; }
}
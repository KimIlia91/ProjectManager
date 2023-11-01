using ErrorOr;
using MediatR;
using PM.Application.Features.EmployeeContext.Dtos;
using PM.Domain.Entities;
using System.Text.Json.Serialization;

namespace PM.Application.Features.EmployeeContext.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommand 
    : IRequest<ErrorOr<DeleteEmployeeResult>>
{
    [JsonIgnore] public Employee? Employee { get; set; }

    public int EmployeeId { get; set; }
}
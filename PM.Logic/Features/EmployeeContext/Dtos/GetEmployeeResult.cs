using Mapster;

namespace PM.Application.Features.EmployeeContext.Dtos;

public sealed record GetEmployeeResult(
     string FirstName,
     string LastName,
     string? MiddelName,
     string Email);
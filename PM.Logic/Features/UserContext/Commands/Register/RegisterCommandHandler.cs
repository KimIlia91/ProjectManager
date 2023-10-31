using ErrorOr;
using MapsterMapper;
using MediatR;
using PM.Application.Common.Interfaces.ISercices;
using PM.Application.Features.UserContext.Dtos;

namespace PM.Application.Features.UserContext.Commands.Register;

public sealed class RegisterCommandHandler 
    : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationUserService _userService;

    public RegisterCommandHandler(
        IApplicationUserService userService,
        IMapper mapper)
    {
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(
        RegisterCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await _userService.RegistrAsync(command);

        if (result.IsError)
            return result.Errors;

        return _mapper.Map<RegisterResult>(result.Value);
    }
}

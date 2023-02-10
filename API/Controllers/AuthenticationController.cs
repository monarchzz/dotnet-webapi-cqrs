using Api.Dtos.Authentication;
using Application.Authentication.Commands.ChangePassword;
using Application.Authentication.Commands.SignUp;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Application.Authentication.Queries.RefreshToken;
using Application.Users.Queries.ProfileQuery;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(SignUpDto dto)
    {
        var command = _mapper.Map<SignUpCommand>(dto);
        var authResult = await _mediator.Send(command);

        return authResult.Match(success => Ok(_mapper.Map<AuthenticationDto>(success)), Problem);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var query = _mapper.Map<LoginQuery>(dto);
        var authResult = await _mediator.Send(query);

        return authResult.Match(success => Ok(_mapper.Map<AuthenticationDto>(success)), Problem);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
    {
        var query = _mapper.Map<RefreshTokenQuery>(dto);
        var authResult = await _mediator.Send(query);

        return authResult.Match(success => Ok(_mapper.Map<AuthenticationDto>(success)), Problem);
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var command = new ChangePasswordCommand(CurrentUser.GetUserId(), dto.CurrentPassword, dto.NewPassword);
        var authResult = await _mediator.Send(command);

        return authResult.Match(_ => Ok(), Problem);
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile()
    {
        var query = new ProfileQuery(CurrentUser.GetUserId());
        var profileResult = await _mediator.Send(query);

        return profileResult.Match(success => Ok(_mapper.Map<ProfileDto>(success)), Problem);
    }
}
using API.Common.Permissions;
using API.Dtos.Role;
using Application.Roles.Queries.RolesById;
using Domain.Common.Authorization;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/roles")]
public class RolesController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RolesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    [Permission(Actions.View, Resources.Roles)]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var query = new RolesByIdQuery(id);
        var roleResult = await _mediator.Send(query);

        return roleResult.Match(success => Ok(_mapper.Map<RoleDto>(success)), Problem);
    }
}
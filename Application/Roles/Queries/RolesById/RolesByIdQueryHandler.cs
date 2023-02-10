using Database.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Application.Roles.Queries.RolesById;

public class RolesByIdQueryHandler : IRequestHandler<RolesByIdQuery, ErrorOr<Role>>
{
    private readonly IRepository<Role> _repository;

    public RolesByIdQueryHandler(IRepository<Role> repository)
    {
        _repository = repository;
    }


    public async Task<ErrorOr<Role>> Handle(RolesByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _repository.Find(request.Id, cancellationToken);

        if (role == null) return Errors.Role.NotExist;

        return role;
    }
}
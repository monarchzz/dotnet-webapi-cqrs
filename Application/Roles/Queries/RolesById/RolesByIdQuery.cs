using Domain.Entities;
using MediatR;
using ErrorOr;

namespace Application.Roles.Queries.RolesById;

public record RolesByIdQuery(Guid Id) : IRequest<ErrorOr<Role>>;
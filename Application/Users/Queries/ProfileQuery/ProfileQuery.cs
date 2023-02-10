using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.ProfileQuery;

public record ProfileQuery(Guid UserId) : IRequest<ErrorOr<User>>;
using Database.Repositories;
using Domain.Common.Errors;
using Domain.Entities;
using MediatR;
using ErrorOr;

namespace Application.Users.Queries.ProfileQuery;

public class ProfileQueryHandler : IRequestHandler<ProfileQuery, ErrorOr<User>>
{
    private readonly IRepository<User> _userRepository;

    public ProfileQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(ProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Find(request.UserId, cancellationToken);
        if (user == null) return Errors.User.NotExist;

        return user;
    }
}
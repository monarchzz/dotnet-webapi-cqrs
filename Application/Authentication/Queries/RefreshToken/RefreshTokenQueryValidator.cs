using FluentValidation;

namespace Application.Authentication.Queries.RefreshToken;

public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenQueryValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
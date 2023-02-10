using Api.Dtos.Authentication;
using Application.Authentication.Common;
using Application.Authentication.Queries;
using Application.Authentication.Queries.Login;
using Mapster;

namespace Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // config.NewConfig<RegisterDto, RegisterCommand>();

        config.NewConfig<LoginDto, LoginQuery>();

        // config.NewConfig<RefreshTokenDto, RefreshTokenQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationDto>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.RefreshToken, src => src.RefreshToken)
            .Map(dest => dest, src => src.User);
    }
}
using API.Common.Errors;
using API.Common.Mapping;
using API.Common.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Converters;

namespace API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    new ObjectResult(new ProblemDetails()
                    {
                        Title = context.ModelState.Where(x => x.Value is not null)
                            .SelectMany(x => x.Value!.Errors.Select(e => e.ErrorMessage))
                            .Distinct()
                            .First()
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                ;
            });
        ;
        services.AddSingleton<ProblemDetailsFactory, DefaultProblemDetailsFactory>();
        services.AddPermissions();
        services.AddMappings();

        return services;
    }

    private static void AddPermissions(this IServiceCollection services)
    {
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }
}
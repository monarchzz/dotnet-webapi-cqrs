using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Initialization;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(
        IServiceProvider serviceProvider
    )
    {
        _serviceProvider = serviceProvider;
    }

    public async Task InitializeDatabasesAsync(CancellationToken cancellationToken)
    {
        // password: 123456
        var user = new User(
            firstName: "admin",
            lastName: "default",
            email: "admin@gmail.com",
            password: "$2a$11$znVuEIpMNCBhgkoVdO2Cn.d2UrxfqpaWPFAGcnT/Tdj6ekgh805Em");

        await InitializeApplicationDbForTenantAsync(user, cancellationToken);
    }

    public async Task InitializeApplicationDbForTenantAsync(User user, CancellationToken cancellationToken)
    {
        // First create a new scope
        using var scope = _serviceProvider.CreateScope();

        // Then run the initialization in the new scope
        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(user, cancellationToken);
    }
}
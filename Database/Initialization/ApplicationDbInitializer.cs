using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Initialization;

public class ApplicationDbInitializer
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ApplicationDbSeeder _dbSeeder;

    public ApplicationDbInitializer(ApplicationDbContext dbContext, ApplicationDbSeeder dbSeeder)
    {
        _dbContext = dbContext;
        _dbSeeder = dbSeeder;
    }

    public async Task InitializeAsync(User user, CancellationToken cancellationToken)
    {
        if (_dbContext.Database.GetMigrations().Any())
        {
            if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                await _dbContext.Database.MigrateAsync(cancellationToken);
                await _dbSeeder.SeedRawQueryAsync(_dbContext, cancellationToken);
            }

            if (await _dbContext.Database.CanConnectAsync(cancellationToken) &&
                !await _dbContext.Users.AnyAsync(cancellationToken))
            {
                await _dbSeeder.SeedDatabaseAsync(user, _dbContext, cancellationToken);
            }
        }
    }
}
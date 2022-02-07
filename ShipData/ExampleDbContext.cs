using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Serilog;
using ShipData.Entities;

namespace ShipData;

public class ExampleDbContext : DbContext
{
    private readonly string _dbPath;

    public ExampleDbContext()
    {
        var folderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        _dbPath = Path.Join(folderPath, "ExpressionsExamplesDb.db");
    }

    public DbSet<Ship> Ships { get; set; }

    public DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseSqlite($"Data Source={_dbPath}")
            .LogTo(Log.Information,
                (eventId, logLevel) => logLevel == LogLevel.Information
                                       && eventId == RelationalEventId.CommandExecuted,
                DbContextLoggerOptions.Category )
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
using ABNLookup.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ABNLookup.Infrastructure
{
    public class AbnContext : DbContext
    {
        private readonly string connectionString;

        // Enables output logging of EF generated queries to the console window
        // Set to true for for debugging purposes - currently set to true when in dev-mode.
        private readonly bool useConsoleLogger;

        public DbSet<Abn> Abns { get; set; }

        public DbSet<MessageCode> MessageCodes { get; set; }

        public AbnContext(string connectionString, bool useConsoleLogger) =>
            (this.connectionString, this.useConsoleLogger) =
            (connectionString, useConsoleLogger);

        public AbnContext(DbContextOptions<AbnContext> options)
            : base(options) =>
                ChangeTracker.QueryTrackingBehavior
                    = QueryTrackingBehavior.NoTracking;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                _ = optionsBuilder.UseSqlite(connectionString);
            }

            if (useConsoleLogger)
            {
                _ = optionsBuilder
                    .UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder
                            .AddFilter((category, level) =>
                                category == DbLoggerCategory.Database.Command.Name
                                && level == LogLevel.Information)
                            .AddConsole();
                    }))
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abn>(model =>
            {
                model.ToTable("Abn")
                    .HasKey(pk => pk.ClientInternalId);
                model.HasIndex(idx => idx.ABNidentifierValue)
                    .IsUnique();
                model.Property(col => col.ACNidentifierValue);
                model.Property(col => col.MainNameorganisationName);
            });

            modelBuilder.Entity<MessageCode>(model =>
            {
                model.ToTable("MessageCode")
                    .HasKey(pk => pk.Code);
                model.Property(col => col.Description);
            });
        }
    }
}
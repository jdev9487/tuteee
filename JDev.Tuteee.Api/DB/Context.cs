namespace JDev.Tuteee.Api.DB;

using Entities;
using Microsoft.EntityFrameworkCore;

public class Context(IConfiguration configuration) : DbContext
{
    protected readonly IConfiguration Configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .ToTable("Client");
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Tutees)
            .WithOne(t => t.Client)
            .HasForeignKey("ClientId");
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Invoices)
            .WithOne(i => i.Client)
            .HasForeignKey("ClientId");

        modelBuilder.Entity<Tutee>()
            .ToTable("Tutee");
        modelBuilder.Entity<Tutee>()
            .HasMany(t => t.Lessons)
            .WithOne(l => l.Tutee)
            .HasForeignKey("TuteeId");
        modelBuilder.Entity<Tutee>()
            .HasMany(t => t.Rates)
            .WithOne(l => l.Tutee)
            .HasForeignKey("TuteeId");

        modelBuilder.Entity<Invoice>()
            .ToTable("Invoice");
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Lessons)
            .WithOne(l => l.Invoice)
            .HasForeignKey("InvoiceId");
        
        modelBuilder.Entity<Lesson>()
            .ToTable("Lesson");
        modelBuilder.Entity<Lesson>()
            .Property(l => l.StartTime)
            .HasConversion(
                dto => dto.ToString("O"),
                str => DateTimeOffset.Parse(str));
        modelBuilder.Entity<Lesson>()
            .Property(l => l.EndTime)
            .HasConversion(
                dto => dto.ToString("O"),
                str => DateTimeOffset.Parse(str));
        
        modelBuilder.Entity<Rate>()
            .ToTable("Rate");
        modelBuilder.Entity<Rate>()
            .Property(r => r.ActiveFrom)
            .HasConversion(
                dto => dto.ToString("O"),
                str => DateTimeOffset.Parse(str));
    }

    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Tutee> Tutees { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
}
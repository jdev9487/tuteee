namespace JDev.Tuteee.DAL;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Context(IConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseLazyLoadingProxies()
            .UseNpgsql(configuration.GetConnectionString("tuteee"), opts =>
            {
                opts.MigrationsAssembly("JDev.Tuteee.Rest.Api");
                opts.UseAdminDatabase("defaultdb");
            });
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

        modelBuilder.Entity<HomeworkAttachment>()
            .ToTable("HomeworkAttachment");

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
            .HasMany(l => l.HomeworkAttachments)
            .WithOne(ha => ha.Lesson)
            .HasForeignKey("LessonId");
        
        modelBuilder.Entity<Rate>()
            .ToTable("Rate");
    }

    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Tutee> Tutees { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<HomeworkAttachment> HomeworkAttachments { get; set; } = default!;
}
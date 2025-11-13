namespace JDev.Tuteee.DAL;

using CustomTypes;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

public class Context(IConfiguration configuration, IOptions<DbConfig> dbConfig) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseLazyLoadingProxies()
            .UseNpgsql(configuration.GetConnectionString("tuteee"), opts =>
            {
                opts.MigrationsAssembly("JDev.Tuteee.Rest.Api");
                opts.UseAdminDatabase(dbConfig.Value.AdminDatabase);
            });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TuitionStakeholder>()
            .ToTable("TuitionStakeholder");
        modelBuilder.Entity<TuitionStakeholder>()
            .HasOne(ts => ts.ClientRole)
            .WithOne(cr => cr.TuitionStakeholder)
            .HasForeignKey<ClientRole>(cr => cr.TuitionStakeholderId);
        modelBuilder.Entity<TuitionStakeholder>()
            .HasOne(ts => ts.TuteeRole)
            .WithOne(cr => cr.TuitionStakeholder)
            .HasForeignKey<TuteeRole>(tr => tr.TuitionStakeholderId);
        modelBuilder.Entity<TuitionStakeholder>()
            .Property(c => c.PhoneNumber)
            .HasConversion(pn => pn.Raw, digits => new PhoneNumber { Raw = digits });
        
        modelBuilder.Entity<ClientRole>()
            .ToTable("ClientRole");
        modelBuilder.Entity<ClientRole>()
            .HasMany(c => c.TuteeRoles)
            .WithOne(t => t.ClientRole)
            .HasForeignKey("ClientRoleId");
        modelBuilder.Entity<ClientRole>()
            .HasMany(c => c.Invoices)
            .WithOne(i => i.ClientRole)
            .HasForeignKey("ClientRoleId");

        modelBuilder.Entity<TuteeRole>()
            .ToTable("TuteeRole");
        modelBuilder.Entity<TuteeRole>()
            .HasMany(t => t.Lessons)
            .WithOne(l => l.TuteeRole)
            .HasForeignKey("TuteeRoleId");
        modelBuilder.Entity<TuteeRole>()
            .HasMany(t => t.Rates)
            .WithOne(l => l.TuteeRole)
            .HasForeignKey("TuteeRoleId");

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
        
        modelBuilder.Entity<HomeworkAttachment>()
            .ToTable("HomeworkAttachment");
    }

    public DbSet<TuitionStakeholder> TuitionStakeholders { get; set; } = default!;
    public DbSet<ClientRole> ClientRoles { get; set; } = default!;
    public DbSet<TuteeRole> TuteeRoles { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<HomeworkAttachment> HomeworkAttachments { get; set; } = default!;
}
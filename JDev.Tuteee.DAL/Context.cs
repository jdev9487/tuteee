namespace JDev.Tuteee.DAL;

using CustomTypes;
using Entities;
using Enums;
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

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.SetIsDeleted();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        this.SetIsDeleted();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stakeholder>()
            .ToTable("Stakeholder");
        modelBuilder.Entity<Stakeholder>()
            .HasOne(ts => ts.ClientRole)
            .WithOne(cr => cr.Stakeholder)
            .HasForeignKey<ClientRole>(cr => cr.StakeholderId);
        modelBuilder.Entity<Stakeholder>()
            .HasOne(ts => ts.TuteeRole)
            .WithOne(cr => cr.Stakeholder)
            .HasForeignKey<TuteeRole>(tr => tr.StakeholderId);
        modelBuilder.Entity<Stakeholder>()
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
        modelBuilder.Entity<TuteeRole>()
            .HasMany(t => t.ReservationSlots)
            .WithOne(rs => rs.TuteeRole)
            .HasForeignKey("TuteeRoleId");

        modelBuilder.Entity<ReservationSlot>()
            .Property(rs => rs.Type)
            .HasConversion(
                x => (int)x,
                y => (ReservationSlotType)y);

        modelBuilder.Entity<Invoice>()
            .ToTable("Invoice");
        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Lessons)
            .WithOne(l => l.Invoice)
            .HasForeignKey("InvoiceId");
        
        modelBuilder.Entity<Lesson>()
            .ToTable("Lesson");
        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.LessonAttachments)
            .WithOne(ha => ha.Lesson)
            .HasForeignKey("LessonId");
        
        modelBuilder.Entity<Rate>()
            .ToTable("Rate");
        
        modelBuilder.Entity<LessonAttachment>()
            .ToTable("LessonAttachment");

        modelBuilder.AddIsDeletedQueryFilter();
    }

    public DbSet<Stakeholder> Stakeholders { get; set; } = default!;
    public DbSet<ClientRole> ClientRoles { get; set; } = default!;
    public DbSet<TuteeRole> TuteeRoles { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<ReservationSlot> ReservationSlots { get; set; } = default!;
    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<LessonAttachment> LessonAttachments { get; set; } = default!;
}
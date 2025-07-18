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
        modelBuilder.Entity<Account>()
            .ToTable("Account");
        modelBuilder.Entity<Account>()
            .HasMany(g => g.Tutees)
            .WithOne(t => t.Account)
            .HasForeignKey("AccountId");

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
        
        modelBuilder.Entity<Homework>()
            .ToTable("Homework");
        modelBuilder.Entity<Homework>()
            .HasMany(h => h.HomeworkAttachments)
            .WithOne(ha => ha.Homework)
            .HasForeignKey("HomeworkId");
        
        modelBuilder.Entity<HomeworkAttachment>()
            .ToTable("HomeworkAttachment");
        
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
    }

    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Tutee> Tutees { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<Homework> Homeworks { get; set; } = default!;
}
using TutorTracker.Api.Entities;

namespace TutorTracker.Api.Context;

using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

    public virtual DbSet<Customer> Customers { get; set; } = default!;
    public virtual DbSet<Student> Students { get; set; } = default!;
    public virtual DbSet<Lesson> Lessons { get; set; } = default!;
}
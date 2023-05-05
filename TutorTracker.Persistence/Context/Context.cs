namespace TutorTracker.Persistence.Context;

using Microsoft.EntityFrameworkCore;
using Entities;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options) {}

    public virtual DbSet<Customer> Customers { get; set; } = default!;
    public virtual DbSet<Student> Students { get; set; } = default!;
    public virtual DbSet<Lesson> Lessons { get; set; } = default!;
}
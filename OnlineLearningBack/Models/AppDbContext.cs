using Microsoft.EntityFrameworkCore;

namespace OnlineLearningBack.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TextBlock> TextBlocks { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(c => c.TextBlocks)
                .WithOne(b => b.Course)
                .HasForeignKey(b => b.CourseId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.TestQuestions)
                .WithOne(t => t.Course)
                .HasForeignKey(t => t.CourseId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedCourses)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);
        }
    }
}

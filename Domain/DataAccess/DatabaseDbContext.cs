using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataAccess
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<ClassModel> ClassModels { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using StudentAttendanceSystem.Models;

namespace StudentAttendanceSystem.Data
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
    }
}

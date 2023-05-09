using Domain.DataAccess;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Domain.Repositories
{
    public class StudentClassRepository : IStudentClassRepository
    {
        private DatabaseDbContext context;

        public StudentClassRepository(DatabaseDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ClassStudent>> GetStudentClassesAsync(int id)
        {
            var studentClasses = context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Include(c => c.Class.Subject)
                .Include(i => i.Class.Instructor)
                .Where(s => s.StudentID == id);


            return await studentClasses.AsNoTracking().ToListAsync();
        }
        // Get Available class - Filtered Class
        public async Task<List<ClassModel>> GetAvailableClassesAsync(int studentId)
        {
            //var cs = await context.ClassStudents.Where(cs => cs.StudentID == studentId).ToListAsync();
            var studentClasses = context.ClassStudents
                .Include(c => c.Class)
                .Include(c => c.Student)
                .Include(c => c.Class.Subject)
                .Include(i => i.Class.Instructor)
                .Where(s => s.StudentID == studentId);

            var classModel = await context.ClassModels
                .Include(c => c.Instructor)
                .Include(j => j.Subject).
                ToListAsync();

            //var classModel = await context.ClassModels
            //    .Include(i => i.Instructor)
            //    .Include(s => s.Subject)
            //    .ToListAsync(); 
            var filteredClasses = from cm in classModel
                                  join sc in studentClasses
                                  on cm.ClassID equals sc.Class.ClassID into filteredClassResult
                                  from fcr in filteredClassResult.DefaultIfEmpty()
                                  where fcr == null
                                  select new { cm };
            List<ClassModel> lookupClasses = new List<ClassModel>();
            foreach (var item in filteredClasses.OrderBy(fc => fc.cm.Subject.SubjectName))
            {
                lookupClasses.Add(new ClassModel
                {
                    ClassID = item.cm.ClassID,
                    ClassName = item.cm.ClassName,
                    ClassSchedule = item.cm.ClassSchedule,
                    ClassTime_End= item.cm.ClassTime_End,
                    ClassTime_Start = item.cm.ClassTime_Start,
                    SubjectID = item.cm.SubjectID,
                    InstructorID = item.cm.InstructorID,
                    Subject = item.cm.Subject,
                    Instructor = item.cm.Instructor,
                });
            }

            return lookupClasses;
        }

        public async Task CreateStudentClassAsync(int studentId, int classId)
        {
            //var studentId = HttpContext.Session.GetInt32(studentId);
            ClassStudent classStudent = new ClassStudent();
            classStudent.StudentID = studentId;
            classStudent.ClassID = classId;
            await context.ClassStudents.AddAsync(classStudent);
            await context.SaveChangesAsync();
        }
        public async Task DeleteStudentClassAsync(int classStudentId) {
            var classStudentModel = await context.ClassStudents.FindAsync(classStudentId);
            if (classStudentModel != null)
            {
                context.ClassStudents.Remove(classStudentModel);
            }

            await context.SaveChangesAsync();
        }
    }
}

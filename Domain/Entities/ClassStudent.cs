using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Domain.Entities
{
    public class ClassStudent
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ClassStudentID { get; set; }

        public int ClassID { get; set; }
        public int StudentID { get; set; }

        public ClassModel Class { get; set; }
        public Student Student { get; set; }
    }
}

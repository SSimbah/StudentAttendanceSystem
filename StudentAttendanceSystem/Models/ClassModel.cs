using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentAttendanceSystem.Models
{
    public class ClassModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ClassID { get; set; }

        [DisplayName("Class Name")]
        [Required(ErrorMessage = "Please Enter Class Name")]
        public string ClassName { get; set; }

        [DisplayName("Subject Code")]
        [Required(ErrorMessage = "Please Enter Subject Code")]
        public string SubjectCode { get; set; }

        [DisplayName("Class Subject")]
        [Required(ErrorMessage = "Please Enter Subject Name")]
        public string SubjectName { get; set; }

        public int SubjectID { get; set; }

        [DisplayName("Class Schedule")]
        [Required(ErrorMessage = "Please Enter Class Schedule")]
        public string ClassSchedule { get; set; }

        public int InstructorID { get; set; }

        [DisplayName("Class Start Time")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Please Enter Class Start Time")]
        public string ClassTime_Start { get; set; }

        [DisplayName("Class End Time")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Please Enter Class End Time")]
        public string ClassTime_End { get; set; }
        public Subject Subject { get; set; }
        public Instructor Instructor { get; set; }
    }
}

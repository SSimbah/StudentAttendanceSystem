using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StudentAttendanceSystem.Models
{
    public class Subject
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SubjectID { get; set; }

        [DisplayName("Subject Code")]
        public string SubjectCode { get; set; }

        [DisplayName("Subject Description")]
        public string SubjectDescription { get; set; }
    }
}

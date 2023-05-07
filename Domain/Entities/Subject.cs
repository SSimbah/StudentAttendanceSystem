using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Subject
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SubjectID { get; set; }

        [DisplayName("Subject Code")]
        public string? SubjectCode { get; set; }

        [DisplayName("Subject Name")]
        public string? SubjectName { get; set; }

        public string SubjectNameExt     // code - name
        {
            get
            {
                return SubjectCode + " - " + SubjectName;
            }
        }

    }
}

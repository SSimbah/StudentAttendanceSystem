using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities
{
    public class Instructor : PersonModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int InstructorID { get; set; }

        [DisplayName("Instructor Number")]
        [Required(ErrorMessage = "Please Enter Instructor Number")]
        public string InstructorNum { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please create your password")]
        public string InstructorPassword { get; set; }

    }
}

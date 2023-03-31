using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities
{
    public class Student : PersonModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int StudentID { get; set; }

        [DisplayName("Student Number")]
        [Required(ErrorMessage = "Please Enter Student Number")]
        public string StudentNum { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please create your password")]
        public string StudentPassword { get; set; }

    }
}

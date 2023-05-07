using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Session
    {
        [Key]
        [ScaffoldColumn(false)]
        public int SessionID { get; set; }
        public int ClassID { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime LateTime { get; set; }
        public bool IsFinal { get; set; }
        public ClassModel? ClassModel { get; set; }
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ClassViewModel
    {
        // View Model from Domain.Entities
        public ClassModel Class { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();

        public List<ScheduleDay> ScheduleDays = new()
        {
            new ScheduleDay{ Day = "Monday"},
            new ScheduleDay{ Day = "Tuesday"},
            new ScheduleDay{ Day = "Wednesday"},
            new ScheduleDay{ Day = "Thursday"},
            new ScheduleDay{ Day = "Friday"},
            new ScheduleDay{ Day = "Saturday"},
            new ScheduleDay{ Day = "Sunday"}
        };
    }
    public class ScheduleDay
    {
        public string Day { get; set; }
    }
}

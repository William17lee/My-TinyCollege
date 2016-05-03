using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mytinycollege.Models;

namespace mytinycollege.ViewModel
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Course { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mytinycollege.Models
{
    public class Student:Person
    {
        public DateTime EnrollmentDate { get; set; }
        
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}
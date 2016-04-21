using System;
using System.Collections.Generic;

namespace mytinycollege.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public int? InstrustorID { get; set; }

        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Course { get; set; }


    }
}
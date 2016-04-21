namespace mytinycollege.Models
{
    public class OfficeAssignment
    {
        public int InstructorID { get; set; }
        public string Location { get; set; }

        public virtual Instructor Instrutor { get; set; }

    }
}
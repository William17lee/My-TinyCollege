using mytinycollege.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace mytinycollege.DAL
{
    public class SchoolContext:DbContext
    {
        //Constructor: Initalize connectionstring (to match web.config)
        public SchoolContext():base("DefaultConnection")
        {

        }

        //Specify Entity Sets - Corresponding to database tables
        //Each single entity corresponds to a row in a table 

        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Department> Departments  { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /*Using Fluent API
             * TakeCare of many-to-many relationships between instructor and course
             * entities. EF Code First can configure this for us, but if we dont 
             * override the name we will het mappings such as 
             * InstructorInstrictorID for the InstructorID column
             */

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
        }
    }
}
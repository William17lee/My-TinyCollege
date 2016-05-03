namespace mytinycollege.Migrations.CollegeMigrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<mytinycollege.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\CollegeMigrations";
        }

        protected override void Seed(mytinycollege.DAL.SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //1. add some student 
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",
                               LastName = "Alexander",
                                EnrollmentDate = DateTime.Parse("2015-02-01"),
                                Email = "calexander@tinycollege.com"},
                new Student { FirstName = "Alonso",
                               LastName = "Alexander",
                                EnrollmentDate = DateTime.Parse("2015-02-01"),
                                Email = "aalexander@tinycollege.com"},
                new Student { FirstName = "Carson",
                               LastName = "Blanc",
                                EnrollmentDate = DateTime.Parse("2015-02-01"),
                                Email = "cblanc@tinycollege.com"},
                new Student { FirstName = "Frank",
                               LastName = "Watson",
                                EnrollmentDate = DateTime.Parse("2015-02-01"),
                                Email = "fwatson@tinycollege.com"},
                new Student { FirstName = "Car",
                               LastName = "Alexa",
                                EnrollmentDate = DateTime.Parse("2015-02-01"),
                                Email = "calexa@tinycollege.com"}
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            //2. add some  inscrutors
            var instructor = new List<Instructor>
            {
                new Instructor { FirstName="Marc",
                                 LastName = "Williams",
                                 HireDate=DateTime.Parse("1996-09-01"),
                                 Email= "mwilliams@tinycollege.com"},
                new Instructor { FirstName="Will",
                                 LastName = "Lee",
                                 HireDate=DateTime.Parse("1996-09-01"),
                                 Email= "wlee@tinycollege.com"},
                new Instructor { FirstName="Brandon",
                                 LastName = "Doodoo",
                                 HireDate=DateTime.Parse("1996-09-01"),
                                 Email= "bdoodoo@tinycollege.com"},
                new Instructor { FirstName="Zach",
                                 LastName = "Leblanc",
                                 HireDate=DateTime.Parse("1996-09-01"),
                                 Email= "zlablanc@tinycollege.com"},
            };
            instructor.ForEach(s => context.Instructors.AddOrUpdate(p => p.Email, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name="Engineering", Budget = 350000,
                                 StartDate=DateTime.Parse("2010-09-01"),
                                 InstructorID= 1},
                new Department { Name="English", Budget = 150000,
                                 StartDate=DateTime.Parse("2010-09-01"),
                                 InstructorID= 2},
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
            //3. add some courses 
            var Course = new List<Course>
            {
                new Course { CourseID = 1045, Title="Chemistry",Credits=3, DepartmentID = 1 },
                new Course { CourseID = 1046, Title="Physics",Credits=3, DepartmentID = 1 },
                new Course { CourseID = 1047, Title="Calculus",Credits=3, DepartmentID = 1 },
                new Course { CourseID = 1048, Title="Literacure",Credits=3, DepartmentID = 2 },
            };
            Course.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();
            //4. add some enrollments 
            var enrollments = new List<Enrollment>
            {
                new Enrollment {StudentID=1, CourseID = 1045, Grade= Grade.A },
                new Enrollment {StudentID=1, CourseID = 1046, Grade= Grade.B },
                new Enrollment {StudentID=2, CourseID = 1045, Grade= Grade.A },
                new Enrollment {StudentID=2, CourseID = 1047, Grade= Grade.C },
                new Enrollment {StudentID=3, CourseID = 1046, Grade= Grade.A },
                new Enrollment {StudentID=3, CourseID = 1048, Grade= Grade.C },
            };
            foreach(Enrollment e in enrollments)
            {
                var enrollmantInDatabase = context.Enrollments.Where(
                    s =>
                    s.StudentID == e.StudentID &&
                    s.course.CourseID == e.CourseID).SingleOrDefault();

                if(enrollmantInDatabase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();

        }
    }
}

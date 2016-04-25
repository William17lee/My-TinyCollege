using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mytinycollege.Models
{
    public class Course
    {

        /*by default the ID property willl become the primary key of the databaase table 
         * that coresponds to this class by default th ef(entity framework)
         * interprets a property thats named ID or ClassNameID as the PK
         * Also, this PK will have an IDENTITY property, tyou can override it using 
         * the DataBaseGeneratedOption enum:
         * -Computed:   Database generates a value when row is inserted or updated
         * -Identity: Databaase generates a value when row is inserted 
         * -None:        Database does not generate values
         */

         [DatabaseGenerated(DatabaseGeneratedOption.None)]
         [Display(Name = "Number")] 
        public int CourseID { get; set; }//PK - Note: with NoIdentity Property

        [StringLength(50, MinimumLength =3)]
        public string Title { get; set; }

        [Range(0,5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual Department Department { get; set; }

        public string CourseIdTitle
        {
            get
            {
                return CourseID + ": " + Title;
            }
        }
    }
}
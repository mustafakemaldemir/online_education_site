using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Student
    {
        public Student()
        {
            CourseStudents = new HashSet<CourseStudent>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }
        public int StudentClass { get; set; }
        public int UserId { get; set; }

        public virtual Cnumber StudentClassNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}

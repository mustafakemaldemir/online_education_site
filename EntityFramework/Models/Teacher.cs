using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Documents = new HashSet<Document>();
            Lessons = new HashSet<Lesson>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
        public int TeacherUserId { get; set; }
        public int TeacherBranchId { get; set; }

        public virtual Branch TeacherBranch { get; set; }
        public virtual User TeacherUser { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}

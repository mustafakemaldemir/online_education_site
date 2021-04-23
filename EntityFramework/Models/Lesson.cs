using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            CourseStudents = new HashSet<CourseStudent>();
            Documents = new HashSet<Document>();
        }

        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public int LessonClassId { get; set; }
        public int? LessonTeacherId { get; set; }

        public virtual Cnumber LessonClass { get; set; }
        public virtual Teacher LessonTeacher { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}

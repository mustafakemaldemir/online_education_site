using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class CourseStudent
    {
        public int LessonId { get; set; }
        public int StudentId { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual Student Student { get; set; }
    }
}

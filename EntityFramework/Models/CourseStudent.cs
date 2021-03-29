using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class CourseStudent
    {
        public int CourseLessonId { get; set; }
        public int CourseStudentId { get; set; }

        public virtual Lesson CourseLesson { get; set; }
        public virtual Student CourseStudentNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int DocumentClass { get; set; }
        public int DocumentLesson { get; set; }
        public int DocumentTeacher { get; set; }
        public DateTime DocumentDate { get; set; }

        public virtual Cnumber DocumentClassNavigation { get; set; }
        public virtual Lesson DocumentLessonNavigation { get; set; }
        public virtual Teacher DocumentTeacherNavigation { get; set; }
    }
}

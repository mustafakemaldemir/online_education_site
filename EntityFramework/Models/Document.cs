using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int DocumentClassId { get; set; }
        public int DocumentLessonId { get; set; }
        public int DocumentTeacherId { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentPrefix { get; set; }

        public virtual Cnumber DocumentClass { get; set; }
        public virtual Lesson DocumentLesson { get; set; }
        public virtual Teacher DocumentTeacher { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Cnumber
    {
        public Cnumber()
        {
            Documents = new HashSet<Document>();
            Lessons = new HashSet<Lesson>();
            Students = new HashSet<Student>();
        }

        public int ClassId { get; set; }
        public int ClassNumber { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}

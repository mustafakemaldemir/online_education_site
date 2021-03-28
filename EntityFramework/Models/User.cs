using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class User
    {
        public User()
        {
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public int UserId { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}

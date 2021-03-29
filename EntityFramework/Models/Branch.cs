using System;
using System.Collections.Generic;

#nullable disable

namespace online_education_site.EntityFramework.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Teachers = new HashSet<Teacher>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class ClaimModel
    {
        public string UserName { get; set; }
        public UserTypes UserType { get; set; }
    }

    public enum UserTypes
    {
        Student,
        Teacher
    }
}

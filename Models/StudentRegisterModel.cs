using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class StudentRegisterModel
    {
        public string user_Email { get; set; }
        public string user_Password { get; set; }
        public string student_Name { get; set; }
        public string student_Surname { get; set; }
        public int student_ClassID { get; set; }

    }
}

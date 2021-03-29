using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class TeacherRegisterModel
    {
        public string user_Email { get; set; }
        public string user_Password { get; set; }
        public string teacher_Name { get; set; }
        public string teacher_Surname { get; set; }
        public string teacher_Branch { get; set; }
    }
}

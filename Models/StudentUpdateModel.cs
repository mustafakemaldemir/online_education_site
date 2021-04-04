using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class StudentUpdateModel
    {
        public string user_Email { get; set; }
        public string new_user_Email { get; set; }
        public string user_Password { get; set; }
        public string new_user_Password { get; set; }        
        public int student_ClassID { get; set; }
    }
}

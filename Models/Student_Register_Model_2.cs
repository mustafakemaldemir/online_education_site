using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using online_education_site.EntityFramework.Models;

namespace online_education_site.Models
{
    public class Student_Register_Model_2
    {
        [Required(ErrorMessage = "E-mail field is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "E-mail")]
        public string user_Email { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Display(Name = "Password")]
        public string user_Password { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        [Display(Name = "Student Name")]
        public string student_Name { get; set; }

        [Required(ErrorMessage = "Surname field is required.")]
        [Display(Name = "Student Surname")]
        public string student_Surname { get; set; }

        public List<Cnumber> Classes { get; set; }

        public int student_ClassID { get; set; }
    }
}

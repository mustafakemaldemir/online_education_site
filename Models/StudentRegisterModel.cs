using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace online_education_site.Models
{
    public class StudentRegisterModel
    {
        [Required(ErrorMessage = "E-mail field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
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

        [Required(ErrorMessage = "Class field is required.")]        
        [Display(Name = "Class")]
        public int student_ClassID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using online_education_site.EntityFramework.Models;

namespace online_education_site.Models
{
    public class TeacherRegisterModel
    {
        [Required(ErrorMessage = "E-mail field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "E-mail")]
        public string user_Email { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Display(Name = "Password")]
        public string user_Password { get; set; }

        [Required(ErrorMessage = "Name field is required.")]
        [Display(Name = "Name")]
        public string teacher_Name { get; set; }

        [Required(ErrorMessage = "Surname field is required.")]
        [Display(Name = "Surname")]
        public string teacher_Surname { get; set; }

        [Required(ErrorMessage = "Branch field is required.")]
        [Display(Name = "Branch")]
        public int teacher_BranchID { get; set; }

        public List<Branch> Branches { get; set; }
    }
}

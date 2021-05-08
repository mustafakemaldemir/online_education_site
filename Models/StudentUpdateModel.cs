using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using online_education_site.EntityFramework.Models;

namespace online_education_site.Models
{
    public class StudentUpdateModel
    {
        [Required(ErrorMessage = "Old e-mail field is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address.")]
        [Display(Name = "Old e-mail")]
        public string user_Email { get; set; }

        [Required(ErrorMessage = "New e-mail field is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid new e-mail address.")]
        [Display(Name = "New e-mail")]
        public string new_user_Email { get; set; }

        [Required(ErrorMessage = "Old password field is required.")]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string user_Password { get; set; }

        [Required(ErrorMessage = "New password field is required.")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [Display(Name = "New password")]
        public string new_user_Password { get; set; }

        [Required(ErrorMessage = "Class field is required.")]        
        [Display(Name = "Class")]
        public int student_ClassID { get; set; }

        public List<Cnumber> Classes { get; set; }
    }
}

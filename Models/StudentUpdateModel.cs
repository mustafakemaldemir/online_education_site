using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace online_education_site.Models
{
    public class StudentUpdateModel
    {
        [Required(ErrorMessage = "Old e-mail field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address.")]
        [Display(Name = "Old e-mail")]
        public string user_Email { get; set; }

        [Required(ErrorMessage = "New e-mail field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid new e-mail address.")]
        [Display(Name = "New e-mail")]
        public string new_user_Email { get; set; }

        [Required(ErrorMessage = "Old password field is required.")]        
        [Display(Name = "Old password")]
        public string user_Password { get; set; }

        [Required(ErrorMessage = "New password field is required.")]        
        [Display(Name = "New password")]
        public string new_user_Password { get; set; }

        [Required(ErrorMessage = "Class field is required.")]        
        [Display(Name = "Class")]
        public int student_ClassID { get; set; }
    }
}

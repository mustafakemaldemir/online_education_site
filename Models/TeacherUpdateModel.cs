using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using online_education_site.EntityFramework.Models;

namespace online_education_site.Models
{
    public class TeacherUpdateModel
    {
        [Required(ErrorMessage = "Old e-mail field is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid old e-mail address.")]
        [Display(Name = "Old e-mail")]
        public string user_Email { get; set; }

        [Required(ErrorMessage = "New e-mail field is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter a valid new e-mail address.")]
        [Display(Name = "New e-mail")]
        public string new_user_Email { get; set; }

        [Required(ErrorMessage = "Old password field is required.")]        
        [Display(Name = "Old password")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string user_Password { get; set; }

        [Required(ErrorMessage = "New password field is required.")]        
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string new_user_Password { get; set; }

        [Required(ErrorMessage = "New branch field is required.")]        
        [Display(Name = "New branch")]
        public int new_user_BranchID { get; set; }

        public List<Branch> Branches { get; set; }

    }
}

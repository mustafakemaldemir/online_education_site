using Microsoft.AspNetCore.Mvc;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Controllers
{
    public class AfterLogin_StudentController : Controller
    {

        private readonly online_educationContext _veritabani;

        public AfterLogin_StudentController(online_educationContext context)
        {
            _veritabani = context;
        }

        public IActionResult UserPage_Student()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_Delete()
        {
            return View();
        }

        [HttpPost]
        public void Student_Update(StudentUpdateModel model) 
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email &&
                 user.UserPassword == model.user_Password);

            if (user != null) 
            {
                var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

                if (student != null) 
                {
                    user.UserEmail = model.new_user_Email;
                    user.UserPassword = model.new_user_Password;
                    student.StudentName = model.student_Name;
                    student.StudentSurname = model.student_Surname;
                    student.StudentClassId = model.student_ClassID;

                    _veritabani.Students.Update(student);
                    _veritabani.SaveChanges();                    
                }

            }
            
        }
    }
}

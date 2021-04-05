using Microsoft.AspNetCore.Mvc;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace online_education_site.Controllers
{
    [Authorize]
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
        public IActionResult Student_Update(StudentUpdateModel model) //If-Else blokları ile dolulukları kontrol et!
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
                    student.StudentClassId = model.student_ClassID;

                    _veritabani.Students.Update(student);
                    _veritabani.SaveChanges();

                    return RedirectUserPage_Student();
                }

                return View();
            }

            return View();
        }

        [HttpDelete]
        public IActionResult Student_Delete(StudentDeleteModel model) //If-Else blokları ile dolulukları kontrol et!
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email &&
                 user.UserPassword == model.user_Password);

            if (user != null)
            {
                var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

                if (student != null)
                {
                    _veritabani.Students.Remove(student);

                    try
                    {
                        _veritabani.SaveChanges();

                        return RedirectUserPage_Student();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        return View();
                    }                    
                }

                else
                    return View();
            }

            else
                return View();
        }

        public IActionResult RedirectUserPage_Student()// UserPage_Student sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(UserPage_Student));
        }
        
    }
}
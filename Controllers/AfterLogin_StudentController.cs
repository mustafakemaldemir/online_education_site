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
using Microsoft.AspNetCore.Authentication;

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

        public IActionResult After_Login_Student_Index()
        {
            return View();
        }

        public IActionResult Account_Settings_Student()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_Update()
        {
            var classes = GetClasses(); // List <CNumber>
            var model = new StudentUpdateModel()
            {
                Classes = classes
            };
            return View(model);
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

                    return Redirect_After_Logın_Student_Index();
                }

                else 
                {
                    ModelState.AddModelError("NotFound", "Student not found!");

                    return Redirect_After_Logın_Student_Index();
                }
                                   
            }

            else 
            {
                ModelState.AddModelError("NotFound", "User not found!");

                return Redirect_After_Logın_Student_Index();
            }
                           
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

                        return RedirectLogoutIndex();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        return View();
                    }                    
                }

                else 
                {
                    ModelState.AddModelError("NotFound", "Student not found!");

                    return View();
                }                    
            }

            else 
            {
                ModelState.AddModelError("NotFound", "User not found!");

                return View();
            }
                
        }

        public List<Cnumber> GetClasses() //Bütün sınıfların döndürülmesi için.
        {
            var classes = _veritabani.Cnumbers.ToList();

            return classes;
        }

        public IActionResult RedirectUserPage_Student()// UserPage_Student sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(UserPage_Student));
        }

        public IActionResult RedirectIndex()// Index sayfasına yönlendirme.
        {
            return RedirectToAction("Index","Home");
        }

        public IActionResult RedirectLogoutIndex()// Logout Index sayfasına yönlendirme.
        {
            return RedirectToAction("Logout", "Home");
        }

        public IActionResult Redirect_After_Logın_Student_Index()// After_Logın_Student_Index sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(After_Login_Student_Index));
        }
    }
}
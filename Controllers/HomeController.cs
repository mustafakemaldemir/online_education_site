using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Controllers
{
    public class HomeController : Controller
    {       

        private readonly online_educationContext _veritabani;

        public HomeController(online_educationContext context)
        {
            _veritabani = context;
        }        

        public IActionResult LOGIN()
        {
            return View();
        }

        public IActionResult REGISTER()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_LOGIN()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Student_REGISTER()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Teacher_LOGIN()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Teacher_REGISTER()
        {
            return View();
        }

        [HttpPost]
        public string Student_LOGIN(LoginModel model)
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email &&
                 user.UserPassword == model.user_Password);
            if (user != null)
            {
                var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

                if (student != null)
                {
                    // giriş başarılı olduktan sonra......
                }

                else 
                {
                                    
                }

                return "Veri tabanında bulunan ogrenci adi: " + student.StudentName ;
            }

            else { return "giris basarısız"; }
            
        }

        [HttpPost]
        public string Teacher_LOGIN(LoginModel model)
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email &&
                 user.UserPassword == model.user_Password);
            if (user != null)
            {
                var teacher = _veritabani.Teachers.FirstOrDefault(teacher => teacher.TeacherUserId == user.UserId);
                if (teacher != null)
                {
                    // giriş başarılı olduktan sonra......
                }

                else
                {

                }

                return "Veri tabanında bulunan ogretmen adi: " + teacher.TeacherName;
            }

            else { return "giris basarısız"; }

        }

        [HttpPost]

        public string Student_REGISTER(StudentRegisterModel model)
        {
            var user = new User()
            {
                UserPassword = model.user_Password,
                UserEmail = model.user_Email
               
            };

            _veritabani.Users.Add(user);
            _veritabani.SaveChanges();

            var student = new Student() 
            {
                StudentName = model.student_Name,
                StudentSurname = model.student_Surname,
                StudentClassId = model.student_ClassID,
                StudentUserId = user.UserId
            };

            _veritabani.Students.Add(student);
            _veritabani.SaveChanges();

            return "Kayıt başarılı";
        }

        public string Teacher_REGISTER(TeacherRegisterModel model)
        {
            var user = new User()
            {
                UserPassword = model.user_Password,
                UserEmail = model.user_Email

            };

            _veritabani.Users.Add(user);
            _veritabani.SaveChanges();

            var teacher = new Teacher()
            {
                TeacherName = model.teacher_Name,
                TeacherSurname = model.teacher_Surname,
                TeacherBranchId = model.teacher_BranchID,
                TeacherUserId = user.UserId
            };

            _veritabani.Teachers.Add(teacher);
            _veritabani.SaveChanges();

            return "Kayıt başarılı";
        }

        public IActionResult RedirectIndex()// Index sayfasına yönlendirme.
        {
            return RedirectToAction("Index");
        }

        public IActionResult RedirectLOGIN()// Login sayfasına yönlendirme.
        {
            return RedirectToAction("LOGIN");
        }

        public IActionResult RedirectREGISTER()// Register sayfasına yönlendirme.
        {
            return RedirectToAction("REGISTER");
        }

        public IActionResult RedirectUserPage_Student()// UserPage_Student sayfasına yönlendirme.
        {
            return RedirectToAction("UserPage_Student");
        }

        public IActionResult RedirectUserPage_Teacher()// UserPage_Teacher sayfasına yönlendirme.
        {
            return RedirectToAction("UserPage_Student");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var branches = _veritabani.Branches.ToList();

            return View(branches);
        }

        [HttpPost]
        public IActionResult Student_LOGIN(LoginModel model) //If-Else blokları ile dolulukları kontrol et!
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email 
                && user.UserPassword == model.user_Password);

            if (user != null)
            {
                var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

                if (student != null)
                {
                    AuthenticateUser(user.UserEmail, UserTypes.Student);

                    return Redirect_After_Login_Student_Index();
                }
                ModelState.AddModelError("NotFound", "Student not found!");

                return View();
            }
            else
            {
                ModelState.AddModelError("NotFound", "User not found!");

                return View();
            }
        }

        [HttpPost]
        public IActionResult Teacher_LOGIN(LoginModel model) //If-Else blokları ile dolulukları kontrol et!
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email 
                && user.UserPassword == model.user_Password);

            if (user != null)
            {
                var teacher = _veritabani.Teachers.FirstOrDefault(teacher => teacher.TeacherUserId == user.UserId);
                if (teacher != null)
                {
                    AuthenticateUser(user.UserEmail, UserTypes.Teacher);

                    return Redirect_After_Login_Teacher_Index();
                }
                ModelState.AddModelError("NotFound", "Instructor not found!");

                return View();
            }

            else
            {
                ModelState.AddModelError("NotFound", "User not found!");

                return View();
            }
        }

        [HttpPost]
        public IActionResult Student_REGISTER(StudentRegisterModel model) //If-Else blokları ile dolulukları kontrol et!
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

            AuthenticateUser(user.UserEmail, UserTypes.Student);

            return Redirect_After_Login_Student_Index();
        }

        [HttpPost]
        public IActionResult Teacher_REGISTER(TeacherRegisterModel model) //If-Else blokları ile dolulukları kontrol et!
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

            AuthenticateUser(user.UserEmail, UserTypes.Teacher);

            return Redirect_After_Login_Teacher_Index();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }

        public IActionResult RedirectIndex()// Index sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RedirectLOGIN()// Login seçim ekranına sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(LOGIN));
        }

        public IActionResult RedirectREGISTER()// Register seçim ekranına sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(REGISTER));
        }

        public IActionResult Redirect_After_Login_Student_Index() // After_Login_Student_Index sayfasına yönlendirme
        {
            return RedirectToAction("After_Login_Student_Index", "AfterLogin_Student");
        }

        public IActionResult Redirect_After_Login_Teacher_Index()// After_Login_Teacher_Index sayfasına yönlendirme
        {
            return RedirectToAction("After_Login_Teacher_Index", "AfterLogin_Teacher");
        }

        public IActionResult RedirectUserPage_Student()// UserPage_Student sayfasına yönlendirme.
        {
            return RedirectToAction("UserPage_Student", "AfterLogin_Student");
        }

        public IActionResult RedirectUserPage_Teacher()// UserPage_Teacher sayfasına yönlendirme.
        {
            return RedirectToAction("UserPage_Teacher", "AfterLogin_Teacher");
        }

        public IActionResult RedirectLessons_Student()// Lessons_Student sayfasına yönlendirme.
        {
            return RedirectToAction("Lessons_Student", "Lessons");
        }
        public IActionResult RedirectLessons_Teacher()// Lessons_Teacher sayfasına yönlendirme.
        {
            return RedirectToAction("Lessons_Teacher", "Lessons");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private async void AuthenticateUser(string userName, UserTypes userType)
        {
            var claim = new ClaimModel { UserName = userName, UserType = userType };

            string jsonString = JsonSerializer.Serialize(claim);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, jsonString) //Guid.NewGuid().ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

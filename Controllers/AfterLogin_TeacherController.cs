using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_education_site.EntityFramework.Models;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace online_education_site.Controllers
{
    [Authorize]
    public class AfterLogin_TeacherController : Controller
    {
        private readonly online_educationContext _veritabani;

        public AfterLogin_TeacherController(online_educationContext context)
        {
            _veritabani = context;
        }

        public IActionResult UserPage_Teacher()
        {
            return View();
        }

        public IActionResult After_Login_Teacher_Index()
        {
            return View();
        }

        public IActionResult Account_Settings_Teacher()
        {
            return View();
        }        

        [HttpGet]
        public IActionResult Teacher_Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Teacher_Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Teacher_Update(TeacherUpdateModel model) //If-Else blokları ile dolulukları kontrol et!
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email &&
                 user.UserPassword == model.user_Password);

            if (user != null)
            {
                var teacher = _veritabani.Teachers.FirstOrDefault(teacher => teacher.TeacherUserId == user.UserId);

                if (teacher != null)
                {
                    user.UserEmail = model.new_user_Email;
                    user.UserPassword = model.new_user_Password;
                    teacher.TeacherBranchId = model.new_user_BranchID;

                    _veritabani.Teachers.Update(teacher);
                    _veritabani.SaveChanges();

                    return Redirect_After_Logın_Teacher_Index();
                }
                else
                    return Redirect_After_Logın_Teacher_Index();

            }
            else
                return Redirect_After_Logın_Teacher_Index();            
        }

        [HttpDelete]
        public IActionResult Teacher_Delete(TeacherDeleteModel model) //If-Else blokları ile dolulukları kontrol et!
        {
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == model.user_Email &&
                 user.UserPassword == model.user_Password);

            if (user != null)
            {
                var teacher = _veritabani.Teachers.FirstOrDefault(teacher => teacher.TeacherUserId == user.UserId);

                if (teacher != null)
                {
                    _veritabani.Teachers.Remove(teacher);

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
                    return View();
            }

            else
                return View();
        }       

        public IActionResult RedirectUserPage_Teacher() //UserPage_Teacher sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(UserPage_Teacher));
        }
        public IActionResult RedirectIndex() //Index sayfasına yönlendirme.
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult RedirectLogoutIndex() //Logout Index sayfasına yönlendirme.
        {
            return RedirectToAction("Logout", "Home");
        }
        public IActionResult Redirect_After_Logın_Teacher_Index() //After_Logın_Teacher_Index sayfasına yönlendirme.
        {
            return RedirectToAction(nameof(After_Login_Teacher_Index));
        }
    }
}

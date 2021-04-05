using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_education_site.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Controllers
{
    [Authorize]
    public class LessonsController : Controller
    {
        private readonly online_educationContext _veritabani;

        public LessonsController(online_educationContext context)
        {
            _veritabani = context;
        }
        public IActionResult Lessons_Student()
        {
            var userName = "";
            var claim = User.Claims.FirstOrDefault();

            if (claim == null)
            {

                return Content("Kullanıcı Bulunamadı");
            }
            userName = claim.Value;
            var user = _veritabani.Users.FirstOrDefault(user => user.UserEmail == userName);

            if (user == null) 
            {
                return Content("Kullanıcı bulunamadı");
            }

            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

            if (student == null) 
            {
                return Content("Kullanıcı bulunamadı");
            }

            var courses = _veritabani.CourseStudents.Where(cs => cs.CourseStudentId == student.StudentId)
                .Select(cs => cs.CourseLesson).ToList();

            return View(courses);
        }

        public IActionResult Lesson(int id) 
        {
            var lesson = _veritabani.Lessons.FirstOrDefault(lesson => lesson.LessonId == id);

            return View(lesson);
        }
    }
}

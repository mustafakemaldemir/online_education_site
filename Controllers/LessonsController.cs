using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_education_site.EntityFramework.Models;
using online_education_site.Helpers;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
           
            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

            if (student == null)
            {
                return Content("Öğrenci bulunamadı!");
            }

            var courses = _veritabani.CourseStudents.Where(cs => cs.CourseStudentId == student.StudentId)
                .Select(cs => cs.CourseLesson).ToList();

            return View(courses);
        }

        public IActionResult Lessons_Teacher()
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            
            var teacher = _veritabani.Teachers.FirstOrDefault(teacher => teacher.TeacherUserId == user.UserId);

            if (teacher == null)
            {
                return Content("Öğretmen bulunamadı");
            }

            var lessons = _veritabani.Lessons.Where(lessonteacher => lessonteacher.LessonTeacherId == teacher.TeacherId)
                .Select(lessonteacher => lessonteacher.LessonName).ToList();

            return View(lessons);
        }

        public IActionResult All_Classes()
        {
            var classes = _veritabani.Cnumbers.Select(classnumber => classnumber.ClassNumber).ToList();
            return View(classes);
        }

        [HttpGet]
        public IActionResult Add_Student_Lesson()
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);

            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);
            var studentLessonIds = _veritabani.CourseStudents
                .Where(cs => cs.CourseStudentId == student.StudentId)
                .Select(cs => cs.CourseLessonId)
                .ToList();

            var lessons = _veritabani.Lessons.Where(l => l.LessonClassId == student.StudentClassId && !studentLessonIds.Contains((int)l.LessonId)).ToList();

            return View(lessons);
        }

        [HttpPost]
        public IActionResult Add_Student_Lesson(int id)
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);

            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

            _veritabani.CourseStudents.Add(new CourseStudent
            {
                CourseLessonId = id,
                CourseStudentId = student.StudentId
            });

            _veritabani.SaveChanges();
            return RedirectToAction("Lessons_Student");
        }

        public IActionResult Lesson(int id)
        {
            var lesson = _veritabani.Lessons.FirstOrDefault(lesson => lesson.LessonId == id);

            return View(lesson);
        }
    }
}
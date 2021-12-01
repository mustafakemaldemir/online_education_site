using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using online_education_site.EntityFramework.Models;
using online_education_site.Helpers;
using online_education_site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace online_education_site.Controllers
{
    [Authorize]
    public class LessonsController : Controller
    {
        private readonly online_educationContext _veritabani;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public LessonsController(online_educationContext context, IWebHostEnvironment environment)
        {
            _veritabani = context;
            _hostingEnvironment = environment;
        }

        public IActionResult All_Lessons()
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

            if (student == null)
            {
                return Content("Öğrenci bulunamadı!");
            }

            var lessons = _veritabani.Lessons.ToList();

            return View(lessons);
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

            var availableLessons = GetAvailableLessons();

            var model = new StudentLessonsModel()
            {
                CurrentLessons = courses,
                AvailableLessons = availableLessons
            };

            return View(model);
        }

        public List<Lesson> GetAvailableLessons()
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

            if (student == null)
            {
                throw new Exception("Ogrenci bulunamadi");
            }

            var studentLessonIds = _veritabani.CourseStudents
                .Where(cs => cs.CourseStudentId == student.StudentId)
                .Select(cs => cs.CourseLessonId)
                .ToList();

            var lessons = _veritabani.Lessons.Where(l => !studentLessonIds.Contains((int)l.LessonId)).ToList();

            return lessons;
        }

        [HttpPost]
        public IActionResult Add_Student_Lesson(int id)
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var student = _veritabani.Students.FirstOrDefault(student => student.StudentUserId == user.UserId);

            if (student == null)
            {
                return Content("Öğrenci bulunamadı!");
            }

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
            var documents = _veritabani.Documents
                .Include(d => d.DocumentLesson)
                .Where(d => d.DocumentLessonId == id)
                .ToList();

            return View(documents);
        }

        public IActionResult Uploaded_documents_byTeacher()
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var teacher = _veritabani.Teachers.FirstOrDefault(t => t.TeacherUserId == user.UserId);

            var documents = _veritabani.Documents
                .Include(d => d.DocumentLesson)
                .Where(d => d.DocumentTeacherId == teacher.TeacherId)
                .ToList();

            return View(documents);
        }

        public List<Lesson> GetTeacherBranches()
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var teacher = _veritabani.Teachers.FirstOrDefault(teacher => teacher.TeacherUserId == user.UserId);


            if (teacher == null)
            {
                throw new Exception("Instructor not found!");
            }

            var branchname = _veritabani.Branches
                .FirstOrDefault(branchname => branchname.BranchId == teacher.TeacherBranchId).BranchName;


            var courses = _veritabani.Lessons
                .Where(cs => cs.LessonName == branchname)
                .ToList();

            return courses;
        }

        public IActionResult Lessons_Teacher()
        {
            var courses = GetTeacherBranches();
            return View(courses);
        }

        public IActionResult Add_Document()
        {
            var courses = GetTeacherBranches();
            return View(courses);
        }

        [HttpGet]
        public IActionResult AddDocumentLesson(int id)
        {
            ViewBag.LessonId = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddDocumentLesson(AddDocumentModel model)
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var teacher = _veritabani.Teachers.FirstOrDefault(t => t.TeacherUserId == user.UserId);
            var lesson = _veritabani.Lessons.FirstOrDefault(l => l.LessonId == model.Id);
            var classNumber = _veritabani.Cnumbers
                .FirstOrDefault(cn => cn.ClassId == lesson.LessonClassId)
                .ClassNumber;

            var random = UploadFile(model.File, lesson.LessonName, classNumber);

            if(random == string.Empty)
            {
                ModelState.AddModelError("Dosya Tipi Hatası", "Bu dosya tipi yüklenemez!");
                return View (); 
            }

            var document = new Document()
            {
                DocumentName = random + "_" + model.File.FileName, // Dosyanon gerçek adı, gösterilirken kullanılcak
                DocumentPrefix = random + "_",
                DocumentRealname = model.File.FileName,
                DocumentClassId = lesson.LessonClassId,
                DocumentLessonId = lesson.LessonId,
                DocumentTeacherId = teacher.TeacherId,
                DocumentDate = DateTime.Now
            };

            _veritabani.Documents.Add(document);
            _veritabani.SaveChanges();

            return RedirectToAction(nameof(Lessons_Teacher));
        }

        private string UploadFile(IFormFile file, string lessonName, int classNumber)
        {
            var acceptedMimeTypes = new List<string>
            {
                "text/plain",
                "application/msword",
                "application/pdf",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "application/vnd.ms-excel",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "image/jpeg",
                "image/png",
                "video/mp4","video/mpeg",
                "application/vnd.ms-powerpoint",
                "application/vnd.openxmlformats-officedocument.presentationml.presentation"
            };

            new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out var mimeType);

            if (!acceptedMimeTypes.Contains(mimeType))
            {
                return string.Empty;
            }

            var folderName = $"{classNumber} - {lessonName}";
            var documentPath = Path.Combine(_hostingEnvironment.WebRootPath, @$"uploaded-documents\{folderName}");
            if (!Directory.Exists(documentPath))
            {
                Directory.CreateDirectory(documentPath);
            }

            var random = RandomString(5);
            var filePath = Path.Combine(documentPath, random + "_" + file.FileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return random;
        }

        public async Task<IActionResult> DownloadFile(string fileName, string folderName, string prefix)
        {
            var fileFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, @$"uploaded-documents\{folderName}");
            if (!Directory.Exists(fileFolderPath))
            {
                return await Task.Run(() => NotFound());
            }
            var fileFolder = new DirectoryInfo(fileFolderPath);

            var file = fileFolder.GetFiles(fileName).FirstOrDefault();
            if (file == null)
            {
                return await Task.Run(() => NotFound());
            }

            var memory = new MemoryStream();
            using (var fileStream = new FileStream(file.FullName, FileMode.Open))
            {
                await fileStream.CopyToAsync(memory);
                memory.Position = 0;
                new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var mimeType);
                var newName = file.Name.Replace(prefix, "");
                return File(memory, mimeType, newName);
            }
        }

        public async Task<IActionResult> Delete_File(int id, string fileName, string folderName, string prefix)
        {
            var claim = User.Claims.FirstOrDefault();
            var user = ClaimHelper.GetUser(claim);
            var teacher = _veritabani.Teachers.FirstOrDefault(t => t.TeacherUserId == user.UserId);

            var document = _veritabani.Documents
                .Include(d => d.DocumentLesson)
                .FirstOrDefault(d => d.DocumentTeacherId == teacher.TeacherId && d.DocumentId == id);

            _veritabani.Documents.Remove(document);

            _veritabani.SaveChanges();

            var fileFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, @$"uploaded-documents\{folderName}");
            if (!Directory.Exists(fileFolderPath))
            {
                return await Task.Run(() => NotFound());
            }
            var fileFolder = new DirectoryInfo(fileFolderPath);

            var file = fileFolder.GetFiles(fileName).FirstOrDefault();
            if (file == null)
            {
                return await Task.Run(() => NotFound());
            }

            file.Delete();

            return RedirectToAction(nameof(Uploaded_documents_byTeacher));
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
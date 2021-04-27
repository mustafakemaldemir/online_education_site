using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_education_site.EntityFramework.Models;

namespace online_education_site.Models
{
    public class StudentLessonsModel
    {
        public List<Lesson> CurrentLessons { get; set; }
        public List<Lesson> AvailableLessons { get; set;}
    }
}

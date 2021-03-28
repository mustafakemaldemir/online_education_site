using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class StudentRegisterModel
    {
        private string user_Email;
        private string user_Password;
        private string student_Name;
        private string student_Surname;
        private int student_ClassID;

        public StudentRegisterModel(string user_Email, string user_Password, string student_Name, string student_Surname, int student_ClassID)
        {
            this.user_Email = user_Email;
            this.user_Password = user_Password;
            this.student_Name = student_Name;
            this.student_Surname = student_Surname;
            this.student_ClassID = student_ClassID;
        }

        public string Getuser_Email()
        {
            return user_Email;
        }

        public void Setuser_Email(string value)
        {
            user_Email = value;
        }        

        public string Getuser_Password()
        {
            return user_Password;
        }

        public void Setuser_Password(string value)
        {
            user_Password = value;
        }        

        public string Getstudent_Name()
        {
            return student_Name;
        }

        public void Setstudent_Name(string value)
        {
            student_Name = value;
        }        

        public string Getstudent_Surname()
        {
            return student_Surname;
        }

        public void Setstudent_Surname(string value)
        {
            student_Surname = value;
        }        

        public int Getstudent_ClassID()
        {
            return student_ClassID;
        }

        public void Setstudent_ClassID(int value)
        {
            student_ClassID = value;
        }
    }
}

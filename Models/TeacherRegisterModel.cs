using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_education_site.Models
{
    public class TeacherRegisterModel
    {
        private string user_Email;
        private string user_Password;
        private string teacher_Name;
        private string teacher_Surname;
        private string teacher_Branch;

        public TeacherRegisterModel(string user_Email, string user_Password, string teacher_Name, string teacher_Surname, string teacher_Branch)
        {
            this.user_Email = user_Email;
            this.user_Password = user_Password;
            this.teacher_Name = teacher_Name;
            this.teacher_Surname = teacher_Surname;
            this.teacher_Branch = teacher_Branch;
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

        public string Getteacher_Name()
        {
            return teacher_Name;
        }

        public void Setteacher_Name(string value)
        {
            teacher_Name = value;
        }

        public string Getteacher_Surname()
        {
            return teacher_Surname;
        }

        public void Setteacher_Surname(string value)
        {
            teacher_Surname = value;
        }

        public string Getteacher_Branch()
        {
            return teacher_Branch;
        }

        public void Setteacher_Branch(string value)
        {
            teacher_Branch = value;
        }

    }
}

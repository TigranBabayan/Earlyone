using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Models
{
    public class Student
    {
        public Student()
        {
            Principals = new List<Principal>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullNameWithLesson => $"{FirstName} {LastName} ({Lesson})";

        public string FullnameWithId => $"{Id} {FirstName} {LastName}";
        public int Score { get; set; }
        public string Lesson { get; set; }

        public int UserId { get; set; }

        public List<Auditorium> Auditoria { get; set; }

        public List<Principal> Principals { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Models
{
    public class Teacher
    {
        public Teacher()
        {
            Principals = new List<Principal>();
        }
        public int Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string FullnameWithId => $"{Id} {FistName} {LastName}";
        public string FullNameWithLesson => $"{FistName} {LastName} ({Lesson})";
        public string Lesson { get; set; }
        public int UserId { get; set; }

        public List<Auditorium> Auditoria { get; set; }

        public List<Principal> Principals { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Models
{
    public class Auditorium
   
    {
        [Key]
        public string LessonName { get; set; }
        [Key]
        public int StudentId { get; set; }

        public Student Student { get; set; }
        [Key]
        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }
}

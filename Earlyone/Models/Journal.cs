using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int Score { get; set; }

        public string Lesson { get; set; }
    }
}

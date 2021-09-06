using Earlyone.Data;
using Earlyone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Earlyone.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationContext _context;

        public StudentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            return View(  _context.Students.Where(st => st.UserId == id).FirstOrDefault());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.LessonsScore = _context.Journal.Include(st => st.Student).Where(st => st.StudentID == id);

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        private List<Lesson> GetLessons()
        {
            var lessons = _context.Lessons.ToList();
            var Alllessons = new List<Lesson>();
            foreach (var lesson in lessons)
            {
                Alllessons.Add(new Lesson() { Id = lesson.Id, LessonName = lesson.LessonName });
            }

            return Alllessons;
        }
    }
}

using Earlyone.Data;
using Earlyone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationContext _context;

        public TeachersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            return View( _context.Teachers.Where(th => th.UserId == id).FirstOrDefault());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.MyStudents = _context.Teachers.Include(au => au.Auditoria).ThenInclude(s=>s.Student).Where(i => i.Id == id).ToList();

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }



        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
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

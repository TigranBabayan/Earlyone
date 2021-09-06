using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Earlyone.Data;
using Earlyone.Models;

namespace Earlyone.Controllers
{
    public class AuditoriumsController : Controller
    {
        private readonly ApplicationContext _context;

        public AuditoriumsController(ApplicationContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Auditoria.Include(a => a.Student).Include(a => a.Teacher);
            return View(await applicationContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.LessonsSelectList = new SelectList(GetLessons(), "LessonName", "LessonName");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullnameWithId");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FullnameWithId");
            return View();
        }

        // POST: Auditoriums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonName,StudentId,TeacherId")] Auditorium auditorium)
        {
            if (ModelState.IsValid)
            {
                _context.Journal.Add(new Journal { StudentID = auditorium.StudentId, Lesson = auditorium.LessonName });
                _context.Add(auditorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", auditorium.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", auditorium.TeacherId);
            return View(auditorium);
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

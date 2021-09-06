using Earlyone.Data;
using Earlyone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Earlyone.Controllers
{
    public class JournalsController : Controller
    {
        private readonly ApplicationContext _context;

        public JournalsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Journals
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Journals.Include(j => j.Student);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            ViewBag.StudentID = new SelectList(_context.Students, "Id", "FullnameWithId", journal.Student);
            ViewBag.Lesson = new SelectList(_context.Students, "Lesson", "Lesson", journal.Student.Lesson);
            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentID,Score,Lesson")] Journal journal)
        {
            if (id != journal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalExists(journal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Teachers");
            }
            ViewData["StudentID"] = new SelectList(_context.Students, "Id", "Id", journal.StudentID);
            return View(journal);
        }


        private bool JournalExists(int id)
        {
            return _context.Journals.Any(e => e.Id == id);
        }
    }
}

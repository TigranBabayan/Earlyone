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
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;
        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(User model)
        {

            User user = await _context.Users.FirstOrDefaultAsync(u => u.FistName == model.FistName && u.Password == model.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("Id", user.Id);
                HttpContext.Session.SetString("UserName", user.FistName);
                if (!string.IsNullOrWhiteSpace(user.Role))
                {
                    HttpContext.Session.SetString("Role", user.Role);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            List<SelectListItem> roles = new List<SelectListItem>()
                {
                    new SelectListItem { Value = "Student", Text = "Student" },
                    new SelectListItem { Value = "Teacher", Text = "Teacher" },
                    new SelectListItem { Value = "Principal", Text = "Principal" },
                };
            ViewBag.roles = roles;
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Registration(User model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    int next;
                    if (_context.Users.ToList().LastOrDefault() == null)
                    {
                       next = 1;
                    }
                    else
                    {
                        next = _context.Users.ToList().LastOrDefault().Id+1;
                    }
                   
                    if (model.Role == "Student")
                    {
                        _context.Students.Add(new Student { FirstName = model.FistName,LastName = model.LastName,UserId=next});
                    }
                   else if(model.Role == "Teacher")
                    {
                        _context.Teachers.Add(new Teacher { FistName = model.FistName, LastName = model.LastName,UserId =next });
                    }
                    else if(model.Role == "Principal")
                    {
                        var principal = _context.Users.AnyAsync(u => u.Role.Contains("Principal"));
                        if (principal.Result == true)
                        {
                            return RedirectToAction("Registration","User");
                        }
                       
                    }
                    _context.Users.Add(new User { FistName = model.FistName,LastName=model.LastName,  Password = model.Password, Role = model.Role });
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "User");
                }
            }
           
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Login", "User");
        }
    }
}

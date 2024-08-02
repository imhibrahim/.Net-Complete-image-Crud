using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace Crud.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext Dbcontext;
        public StudentController(ApplicationDbContext student)
        {
            this.Dbcontext = student;
        }
        //Student View Working start
        public IActionResult Index()
        {
            //var students = new List<student> {
            //new student{id=1,name="ibrahim",address="korangi",Email="ibrahim@gmail.com"}
            //};
            // return View (students);
            var list = Dbcontext.student.ToList();
            return View(list);

        }
        //Student View Working end

        //--!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!--

        //Student create Working start
        public IActionResult Create()
        { return View();
        }

        [HttpPost]
        public IActionResult Create(student std)
        { if (ModelState.IsValid)
            {
                var Student = new student
                {
                    name = std.name,
                    address = std.address,
                    Email = std.Email,
                };
                Dbcontext.student.Add(Student);
                Dbcontext.SaveChanges();
                return RedirectToAction("index");
            }
            return View(std);
        }
        //Student create Working end

        //!-------------------------!

        //Student Edit Working start
        public IActionResult edit(int id)
        {
            var std = Dbcontext.student.FirstOrDefault(student => student.id == id);
            return View(std);
        }
        [HttpPost]
        public IActionResult edit(int id, student model)     
        {
            
            if (id != model.id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var std = Dbcontext.student.FirstOrDefault(student => student.id == id);
                if (std != null)
                {
                    std.name = model.name;
                    std.address = model.address;
                    std.Email = model.Email;

                    Dbcontext.SaveChanges();
                    return RedirectToAction("Index","Student");
                }
                else
                {
                    return NotFound();
                }
            }
            return View(model);
        }
        //Student Edit Working End

        //!-------------------------!

        //Student Delete Working start
        public IActionResult delete(int id)
        {
            var std = Dbcontext.student.FirstOrDefault(student => student.id == id);
            if(std != null)
            {
                Dbcontext.student.Remove(std);
                Dbcontext.SaveChanges();
            }
            return RedirectToAction("index","Student");
        }
        //Student Delete Working start

    }
}

using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    public class StudentDataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentDataController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this._context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var data = _context.student_Data.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student_data model, IFormFile pic)
        {
            if (ModelState.IsValid)
            {
                if (pic != null && pic.Length > 0)
                {
                    var fileimage = Guid.NewGuid().ToString() + "_" + pic.FileName;
                    var filepath = Path.Combine(_env.WebRootPath, "images", fileimage);

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        pic.CopyTo(stream);
                    }

                    model.pic = "/images/" + fileimage;
                }

                var data = new Student_data
                {
                    name = model.name,
                    Email = model.Email,
                    Standard = model.Standard,
                    pic = model.pic
                };

                // Add logic to save 'data' to the database
                _context.student_Data.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var data = _context.student_Data.FirstOrDefault(stdid => stdid.id == id);

            if (data == null)
            {
                return NotFound();
            }

            ViewData["Image"] = data.pic;

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(int id, Student_data model, IFormFile imageFile)
        {
            if (id != model.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = _context.student_Data.FirstOrDefault(stdid => stdid.id == id);
                if (data == null)
                {
                    return NotFound();
                }

                // Update textual data fields
                data.name = model.name;
                data.Email = model.Email;
                data.Standard = model.Standard;

                if (imageFile != null && imageFile.Length > 0)
                {
                    // Handle new image upload
                    var fileimage = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    var filepath = Path.Combine(_env.WebRootPath, "images", fileimage);

                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // Delete old image if a new one is uploaded
                    if (!string.IsNullOrEmpty(data.pic))
                    {
                        var oldImagePath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(data.pic));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    data.pic = "/images/" + fileimage;
                }
                else
                {
                    // No new image uploaded, retain the existing image path
                    data.pic = data.pic;
                }

                // Update entity in database
                _context.student_Data.Update(data);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model); // Return the model to the view with validation errors
        }




        public IActionResult Delete(int id)
        {

            var data = _context.student_Data.FirstOrDefault(b => b.id == id);
            if (data == null)
            {
                return NotFound();
            }
            var imagePath = Path.Combine(_env.WebRootPath, data.pic.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.student_Data.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("index");

        }


    }
}

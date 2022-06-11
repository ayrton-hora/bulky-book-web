using Microsoft.AspNetCore.Mvc;

using BulkyBookWeb.Data;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _database;

        public CategoryController(ApplicationDbContext db)
        {
            _database = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _database.Categories.ToList();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.Equals(obj.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _database.Categories.Add(obj);
                _database.SaveChanges();
                TempData["Sucess"] = "Category created successfully.";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var category = _database.Categories.Find(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name.Equals(obj.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _database.Categories.Update(obj);
                _database.SaveChanges();
                TempData["Sucess"] = "Category Edited successfully.";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var category = _database.Categories.Find(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _database.Categories.Find(id);
            if (obj == null) return NotFound();

            _database.Categories.Remove(obj);
            _database.SaveChanges();
            TempData["Sucess"] = "Category deleted successfully.";
            return RedirectToAction("Index"); 
        }
    }
}

using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
            ModelState.AddModelError("Name", "Name and Display order cannot be same .");
            }
         
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                //return View("Index", _db.Categories.ToList());
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Edit(int ? id)
        {
            if(id == null  )
            {
                return NotFound();
            }
            Category ? categoriesFromDb = _db.Categories.Find(id);
            //Category ? categoriesFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category ? categoriesFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if(categoriesFromDb == null)
            {
                return NotFound();
            }
            return View(categoriesFromDb);          
        }

        [HttpPost]

        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}

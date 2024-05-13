using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display order cannot be same .");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                //return View("Index", _db.Categories.ToList());
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? categoriesFromDb = _unitOfWork.Category.Get(u => u.Id==id);
            //Category ? categoriesFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category ? categoriesFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoriesFromDb == null)
            {
                return NotFound();
            }
            return View(categoriesFromDb);
        }

        [HttpPost]

        public IActionResult Edit(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "Name and DisplayOrder cannot be matched .");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
                //return View("Index", _db.Categories.ToList());
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category? CategoryFromDb = _unitOfWork.Category.Get(u => u.Id ==id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get( u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Deleted Successfully";
                //return View("Index", _db.Categories.ToList());
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}

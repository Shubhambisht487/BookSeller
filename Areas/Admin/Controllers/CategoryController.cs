using BookSeller.DataAccess.Data;
using BookSeller.DataAccess.Repository.IRepository;
using BookSeller.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSeller.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public CategoryController(ApplicationDbContext context)
        //{
        //        this._context = context;
        //}
        //private readonly ICategoryRepository _categoryRepo;
        //public CategoryController(ICategoryRepository categoryRepo)
        //{
        //    this._categoryRepo = categoryRepo;
        //}
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Category> item = _context.Categories.ToList();
            IEnumerable<Category> item = _unitOfWork.Category.GetAll();
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("","Name and Display Order could not be same");

            //}
            //if(obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("","It is an Invalid Name");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.Get(c => c.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);

            return View(obj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? obj = _unitOfWork.Category.Get(x => x.Id == id);
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

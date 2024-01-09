using BookSeller.DataAccess.Repository.IRepository;
using BookSeller.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSeller.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;              
        }
        public IActionResult Index()
        {
            var item = _unitOfWork.Product.GetAll();
            return View(item);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully";
                return RedirectToAction("Index");   
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u=> u.Id == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if(product== null)
            { 
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Product? obj = _unitOfWork.Product.Get(u=> u.Id==id);
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Deleted Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

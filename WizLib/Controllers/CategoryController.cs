using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        //dependancy injection
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db )
        {
            _db = db; 
        }



        public IActionResult Index()
        {
            //display all the categories - converson ToList
            List<tb_Category> objlist = _db.tb_Category.ToList();

            return View(objlist);
        }

        //create new / edit category 
        public IActionResult Upsert(int ?id)
        {
			tb_Category obj = new tb_Category();
			if (id == null)
			{
				return View(obj);
			}
			//this for edit
			obj = _db.tb_Category.FirstOrDefault(u => u.CategoryId == id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);

		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(tb_Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.CategoryId == 0)
                {
                    //this is create
                    _db.tb_Category.Add(obj);
                }
                else
                {
                    //this is an update
                    _db.tb_Category.Update(obj);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }



        public IActionResult Delete(int id)
        {
            var objFromDb = _db.tb_Category.FirstOrDefault(u => u.CategoryId == id);
            _db.tb_Category.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple2()
        {
            List<tb_Category> catList = new List<tb_Category>();
            for (int i = 1; i <= 2; i++)
            {
                catList.Add(new tb_Category { CategoryName = Guid.NewGuid().ToString() });
                //_db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _db.tb_Category.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            List<tb_Category> catList = new List<tb_Category>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new tb_Category { CategoryName = Guid.NewGuid().ToString() });
                //_db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _db.tb_Category.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveMultiple2()
        {
            IEnumerable<tb_Category> catList = _db.tb_Category.OrderByDescending(u => u.CategoryId).Take(2).ToList();

            _db.tb_Category.RemoveRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            IEnumerable<tb_Category> catList = _db.tb_Category.OrderByDescending(u => u.CategoryId).Take(5).ToList();

            _db.tb_Category.RemoveRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }





    }
}

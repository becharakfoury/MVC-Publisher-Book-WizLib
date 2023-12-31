﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WizLib_DataAccess.Data;
using WizLib_DataAccess.Migrations;
using WizLib_Model.Models;
using WizLib_Model.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        //dependancy injection
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db )
        {
            _db = db; 
        }



        public IActionResult Index()
        {

           List<Book> objlist = _db.Books.Include(u => u.Publisher)
                           .Include(u => u.BookAuthors).ThenInclude(u => u.Author).ToList();
            //ThenInclude ==> To Define a reference to BookAuthors not Books    

            //display all the categories - converson ToList
            //List<Book> objlist = _db.Books.ToList();
            //List<Book> objlist = _db.Books.Include(u => u.Publisher).ToList();

            //List<Book> objList = _db.Books.ToList();
            // foreach (var obj in objList)
            // {
            //    //Least Effecient
            //    //obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_id);
            //    //Explicit Loading More Efficient
            //   _db.Entry(obj).Reference(u => u.Publisher).Load();
            //    _db.Entry(obj).Collection(u => u.BookAuthors).Load();
            //   foreach(var bookAuth in obj.BookAuthors)
            //   {
            //       _db.Entry(bookAuth).Reference(u => u.Author).Load();
            //   }
            // }


            return View(objlist);
        }

        //create new / edit. Dispaly Biook Info (Get Method)        
        public IActionResult Upsert(int ?id)
        {
            BookVM obj = new BookVM();
            //System.Web.Mvc.
            obj.PublisherList = _db.Publishers.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });
            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);      

        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {
            if (obj.Book.Book_Id == 0)
            {
                //this is create
                _db.Books.Add(obj.Book);
            }
            else
            {
                //this is an update                    
                _db.Books.Update(obj.Book);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        //************ Book Details***********************

        public IActionResult Details(int? id)
        {
            BookVM obj = new BookVM();
        
            if (id == null)
            {
                return View(obj);
            }
            //this for edit / return book and bookDetails
            //obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            //populate also book Details
            //obj.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_id == id);

            obj.Book = _db.Books
                .Include(u => u.BookDetail)
                .FirstOrDefault(u => u.Book_Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM obj)
        {
            if (obj.Book.BookDetail.BookDetail_id == 0)
            {
                //this is create
                _db.BookDetails.Add(obj.Book.BookDetail);
                _db.SaveChanges();

                //Bring book detail ID and Assign it to the book
                var BookFromDB = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id);
                BookFromDB.BookDetail_id = obj.Book.BookDetail.BookDetail_id;


                _db.SaveChanges();
            }
            else
            {
                //this is an update                    
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();
            }
         
            return RedirectToAction(nameof(Index));

        }

        //************ book Details **********************

        public IActionResult Delete(int id)
      {
          var objFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == id);
          _db.Books.Remove(objFromDb);
          _db.SaveChanges();
          return RedirectToAction(nameof(Index));
      }

        public IActionResult PlayGround()
        {
            var bookTemp = _db.Books.FirstOrDefault();
            bookTemp.Price = 100;

            var bookCollection = _db.Books;
            double totalPrice = 0;

            foreach (var book in bookCollection)
            {
                totalPrice += book.Price;
            }

            var bookList = _db.Books.ToList();
            foreach (var book in bookList)
            {
                totalPrice += book.Price;
            }

            var bookCollection2 = _db.Books;
            var bookCount1 = bookCollection2.Count();

            var bookCount2 = _db.Books.Count();



            var bootktemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 2);
            bootktemp1.BookDetail.NumberOfChapters = 2555;
            _db.Books.Update(bootktemp1);
            _db.SaveChanges();

            var bootktemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 2);
            bootktemp2.BookDetail.Weigth = 2555;
            _db.Books.Attach(bootktemp2);
            _db.SaveChanges();


             
            return RedirectToAction(nameof(Index));
        }

        //Get Method for manage authors
        public IActionResult ManageAuthors(int id)
        {

            BookAuthorVM obj = new BookAuthorVM
            {
                BookAuthorList = _db.BookAuthors
                 .Include(u => u.Author)
                 .Include(u => u.Book)
                 .Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new BookAuthor() //when create new bookAuthor we assign this is
                {
                    Book_Id = id
                },
                Book = _db.Books.FirstOrDefault(u => u.Book_Id == id)
                //get info of current book, object needed when we create a new book
                //avoid BookAuthors to be empty
            };

            //NOT IN Clause in LINQ
            //get all the authors whos id is not in tempListOfAssignedAuthors
            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(u => u.Author_Id).ToList();

            //Bring all bookauthor not assigned, not existed in the above list
            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();

            //Bind the drop doww
            obj.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });


            return View(obj);
        }


        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }            
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });

        }

        //RemoveAuthors
        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.Book_Id;//hidden fields

            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(
                u => u.Author_Id == authorId && u.Book_Id == bookId);

            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });

        }

    }
}

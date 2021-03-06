﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekt_ASP.Helpers;
using Projekt_ASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Projekt_ASP.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public BookController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            BookViewModel viewModel = new BookViewModel();
            return View("Index", viewModel);
        }
        public IActionResult Edit(int id)
        {
            BookViewModel viewModel = new BookViewModel();
            string uniqueFileName = UploadedFile(viewModel);

            viewModel.EditableItem.ImagePath = uniqueFileName;
            viewModel.EditableItem = viewModel.Bookshelf.FirstOrDefault(obj => obj.Id == id);

            return View("Index", viewModel);
        }

        [AllowAnonymous]
        public IActionResult Info(int id)
        {
            BookViewModel viewModel = new BookViewModel();
            viewModel.EditableItem = viewModel.Bookshelf.FirstOrDefault(obj => obj.Id == id);
            return View("Info", viewModel);
        }

        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                Book item = db.Get<Book>(id); //Dapper method for finding the requested item in the database
                if (item != null)
                    db.Delete(item); //Dapper method for deleting the item from the db
                return RedirectToAction("Index");
            }
        }

        public IActionResult CreateUpdate(BookViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(viewModel);

                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.ImagePath = uniqueFileName;
                        db.Insert<Book>(viewModel.EditableItem); //Dapper method for inserting an item to the db
                    }
                    else
                    {
                        Book dbItem = db.Get<Book>(viewModel.EditableItem.Id);
                        var result = TryUpdateModelAsync<Book>(dbItem, "EditableItem");
                        db.Update<Book>(dbItem); //Dapper method for updating the db
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new BookViewModel());
        }

        public IActionResult ToggleIsRead(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                Book item = db.Get<Book>(id);
                if (item != null)
                {
                    item.IsRead = !item.IsRead;
                    db.Update<Book>(item);
                }
            }
            return RedirectToAction("Index");
        }

        private string UploadedFile (BookViewModel viewModel)
        {
            string uniqueFileName = null;

            if(viewModel.file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

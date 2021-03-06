﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Projekt_ASP.Helpers;
using Projekt_ASP.Models;

namespace Projekt_ASP.Models
{
    public class BookViewModel
    {
        public BookViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new Book();
                this.Bookshelf = db.Query<Book>("SELECT * FROM Bookshelf ORDER BY Author ASC").ToList();
            }
        }

        public List<Book> Bookshelf { get; set; }
        public Book EditableItem { get; set; }

        public IFormFile file { get; set; }
    }
}

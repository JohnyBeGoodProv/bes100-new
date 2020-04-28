using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class BooksController : Controller
    {
        LibraryDataContext Context;

        public BooksController(LibraryDataContext context)
        {
            Context = context;
        }

        [HttpGet("books")]
        public ActionResult GetAllBooks([FromQuery] string genre)
        {
            var books = Context.Books
                .Where(b => b.InStock)
                .Select(b => new GetBooksResponseItem
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    NumberOfPages = b.NumberOfPages
                });   ;

            if (genre != null)
            {
                books = books.Where(b => b.Genre == genre);
            }

            var booksList = books.ToList();
            var response = new GetBooksResponse
            {
                Books = booksList,
                GenreFilter = genre,
                NumberOfBooks = booksList.Count
            };

            return Ok(response);
        }
    }
}

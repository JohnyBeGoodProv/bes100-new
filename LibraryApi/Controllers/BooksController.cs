using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPut("books/{id:int}/numberofpages")]
        public async Task<ActionResult> ChangeNumberOfPages(int id, [FromBody] int numberOfPages)
        {

            if (numberOfPages <= 0)
            {
                return BadRequest("Number of Pages should be an integer greater than 0");
            }

            var book = await Context.Books
                 .Where(b => b.Id == id && b.InStock)
                 .SingleOrDefaultAsync();

            if (book !=null)
            {
                book.NumberOfPages = numberOfPages;
                await Context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("books/{bookId:int}")]
        public async Task<ActionResult> RemoveABook(int bookId)
        {
            var bookToRemove = await Context.Books
                .Where(b => b.InStock && b.Id == bookId)
                .SingleOrDefaultAsync();

            if (bookToRemove != null)
            {
                bookToRemove.InStock = false;
                await Context.SaveChangesAsync();
            }
            return NoContent(); // Fine

        }



        [HttpPost("books")]
        public async Task<ActionResult> AddABook([FromBody] PostBookCreate bookToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                Title = bookToAdd.Title,
                Author = bookToAdd.Author,
                Genre = bookToAdd.Genre,
                NumberOfPages = bookToAdd.NumberOfPages,
                InStock = true
            };

            Context.Books.Add(book);  // I have no Id!
            await Context.SaveChangesAsync();   // Suddenly I have an Id!

            var response = new GetABookResponse   
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                NumberOfPages = book.NumberOfPages
            };

            return CreatedAtRoute("books#getabook", new { bookId = response.Id }, response);

        }
        /// <summary>
        /// Retrieve one of our books
        /// </summary>
        /// <param name="bookId">The Id of the book you want to find</param>
        /// <returns>A book</returns>
        [HttpGet("books/{bookId:int}", Name ="books#getabook")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetABookResponse>> GetABook(int bookId)
        {
            var book = await Context.Books
                .Where (b=>b.InStock && b.Id == bookId)
                .Select(b=> new GetABookResponse
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = b.Genre,
                    NumberOfPages = b.NumberOfPages
                }).SingleOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
        }

        [HttpGet("books")]
        public  async Task<ActionResult> GetAllBooks([FromQuery] string genre)
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

            var booksList = await books.ToListAsync();
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

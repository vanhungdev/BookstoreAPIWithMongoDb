using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure.Entities;
using Bookstore.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Application.Category;
using Bookstore.Application.Book;
using Bookstore.Infrastructure.Exceptions;
using Bookstore.Infrastructure.Utilities;
using Bookstore.Infrastructure.Enums;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookMongoDb _book;
        public BooksController(BookMongoDb category)
        {
            _book = category;
        }

        [HttpGet("GetAll")]
        public ActionResult getAllBook()
        {
            return Ok(_book.get());
        }
        [HttpGet("Get/{id}")]
        public ActionResult<Book> Get(string id)
        {
            var data = _book.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpPost("Create")]
        public ActionResult Create([FromBody] Book book)
        {
            var result = _book.Create(book);
            return Ok("Create successfully");
        }
        [HttpPost("Edit/{id}")]
        public IActionResult Update(string id, Book categoryin)
        {
            var category = _book.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            var data = _book.Update(id, categoryin);
            if (!data)
            {
                return Ok("Something is wrong ");
            }
            return Ok("Update successfully ");
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var book = _book.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            var data = _book.Remove(book.Id);
            if (!data)
            {
                return Ok("Something is wrong ");
            }
            return Ok("Deleted successfully ");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Application.Category;
using Bookstore.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryMongoDb _category;
        public CategoryController(CategoryMongoDb category)
        {
            _category = category;
        }
        [HttpGet("GetAll")]
        public ActionResult getAllBook()
        {
            return Ok(_category.get());
        }
        [HttpGet("Get/{id}")]
        public ActionResult<Category> Get(string id)
        {
            var data = _category.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return data;
        }
        [HttpPost("Create")]
        public ActionResult Create([FromBody] Category book)
        {
            var result = _category.Create(book);
            return Ok("Create successfully");
        }
        [HttpPost("Edit/{id}")]
        public IActionResult Update(string id, Category categoryin)
        {
            var category = _category.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            var data = _category.Update(id, categoryin);
            if (!data)
            {
                return Ok("Something is wrong ");
            }
            return Ok("Update successfully ");
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var category = _category.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            var data = _category.Remove(category.Id);
            if (!data)
            {
                return Ok("Something is wrong ");
            }
            return Ok("Deleted successfully ");
        }
    }
}

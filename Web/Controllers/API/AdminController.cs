using Domain.Domain_Models;
using Domain.Identity_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers.API
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IService<Book> _service;

        public AdminController(IService<Book> service)
        {
            _service = service;
        }

        [HttpGet("books")]
        public IActionResult GetAllBooks()
        {
            var books = _service.GetAll();
            if (books == null || !books.Any())
            {
                return NotFound("No Students");
            }
            return Ok(books);
        }

        [HttpGet("books/{id}")]
        public IActionResult GetBookById(Guid id)
        {
            var book = _service.GetById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}

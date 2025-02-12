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
        private readonly UserManager<IntegratedSystemsUser> _userManager;
        private readonly IService<Book> _service;

        public AdminController(UserManager<IntegratedSystemsUser> userManager, IService<Book> service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpGet("books")]
        public IActionResult GetBooks()
        {
            var books = _service.GetAll();
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

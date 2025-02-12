using Domain.Domain_Models;
using Domain.Identity_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IService<Book> _service;

        public AdminController(IService<Book> service)
        {
            _service = service;
        }

        [HttpGet("[action]")]
        public List<Book> GetAllBooks()
        {
            return _service.GetAll();
        }

        [HttpPost("[action]")]
        public Book GetDetails(Guid id)
        {
            return _service.GetById(id);
        }
    }
}

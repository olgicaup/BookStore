using Domain.Domain_Models;
using Domain.Identity_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<IntegratedSystemsUser> _userManager;
        public AdminController(IOrderService orderService, UserManager<IntegratedSystemsUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return this._orderService.GetOrders();
        }
        [HttpPost("[action]")]
        public Order GetDetails(BaseEntity id)
        {
            return this._orderService.GetDetails(id);
        }
    }
}

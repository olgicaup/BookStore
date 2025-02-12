using Domain.Domain_Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public Order GetDetails(BaseEntity id)
        {
            return this._orderRepository.GetDetailsForOrder(id);
        }

        public List<Order> GetOrders()
        {
            return this._orderRepository.GetAllOrders().ToList();
        }
    }
}

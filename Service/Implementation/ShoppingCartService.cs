using Domain.Domain_Models;
using Domain.Domain_Models.DTO;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<BookInShoppingCart> _bookInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<BookInShoppingCart> bookInShoppingCartRepository, IUserRepository userRepository, IRepository<Order> orderRepository, IRepository<BookInOrder> bookInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _bookInShoppingCartRepository = bookInShoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _bookInOrderRepository = bookInOrderRepository;
        }

        public bool AddToShoppingConfirmed(BookInShoppingCart model, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser.ShoppingCart;

            if (userShoppingCart.BookInShoppingCarts == null)
                userShoppingCart.BookInShoppingCarts = new List<BookInShoppingCart>(); ;

            userShoppingCart.BookInShoppingCarts.Add(model);
            _shoppingCartRepository.Update(userShoppingCart);
            return true;
        }

        public bool deleteBookFromShoppingCart(string userId, Guid bookId)
        {
            if (bookId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;
                var book = userShoppingCart.BookInShoppingCarts.Where(x => x.BookId == bookId).FirstOrDefault();

                userShoppingCart.BookInShoppingCarts.Remove(book);

                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser?.ShoppingCart;
            var allBook = userShoppingCart?.BookInShoppingCarts?.ToList();

            var totalPrice = allBook.Select(x => (x.Book.Price * x.Quantity)).Sum();


            ShoppingCartDto dto = new ShoppingCartDto
            {
                Books = allBook,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    userId = userId,
                    Owner = loggedInUser,
                    BooksInOrder = new List<BookInOrder>()
                };

                _orderRepository.Insert(order);

                List<BookInOrder> bookInOrder = new List<BookInOrder>();

                var lista = userShoppingCart.BookInShoppingCarts.Select(
                    x => new BookInOrder
                    {
                        Id = Guid.NewGuid(),
                        BookId = x.Book.Id,
                        Book = x.Book,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();


                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= lista.Count(); i++)
                {
                    var currentItem = lista[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Book.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Book.BookName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Book.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                //message.Content = sb.ToString();

                bookInOrder.AddRange(lista);

                foreach (var book in bookInOrder)
                {
                    _bookInOrderRepository.Insert(book);
                }

                loggedInUser.ShoppingCart.BookInShoppingCarts.Clear();
                _userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}

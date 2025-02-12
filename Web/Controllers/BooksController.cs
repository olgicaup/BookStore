using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Domain_Models;
using Repository;
using Service.Interface;
using System.Security.Claims;
using Service.Implementation;

namespace Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IPdfService _pdfService;
        private readonly IService<Book> _bookService;
        private readonly IService<Author> _authorService;
        private readonly IService<Publisher> _publisherService;
        private readonly IShoppingCartService _shoppingCartService;

        public BooksController(IOrderService orderService, IPdfService pdfService, IService<Book> bookService, IService<Author> authorService, IService<Publisher> publisherService, IShoppingCartService shoppingCartService)
        {
            _orderService = orderService;
            _pdfService = pdfService;
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
            _shoppingCartService = shoppingCartService;
        }



        // GET: Books
        public IActionResult Index()
        {
            var books = _bookService.GetAll();
            return View(books);
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["PublisherName"] = new SelectList(_publisherService.GetAll(), "Id", "PublisherName");
            ViewData["AuthorName"] = new SelectList(_authorService.GetAll(), "Id", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BookName,BookDescription,BookImage,Price,Rating,AuthorId,PublisherId,Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                _bookService.CreateNew(book);

                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorName"] = new SelectList(_authorService.GetAll(), "Id", "FullName", book.AuthorId);
            ViewData["PublisherName"] = new SelectList(_publisherService.GetAll(), "Id", "PublisherName", book.PublisherId);

            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["PublisherName"] = new SelectList(_publisherService.GetAll(), "Id", "PublisherName");
            ViewData["AuthorName"] = new SelectList(_authorService.GetAll(), "Id", "FullName");
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("BookName,BookDescription,BookImage,Price,Rating,AuthorId,PublisherId,Id")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorName"] = new SelectList(_authorService.GetAll(), "Id", "FullName", book.AuthorId);
            ViewData["PublisherName"] = new SelectList(_publisherService.GetAll(), "Id", "PublisherName", book.PublisherId);

            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var book = _bookService.GetById(id);
            if (book != null)
            {
                _bookService.Delete(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return _bookService.GetById(id) != null;
        }

        public IActionResult AddToCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetById(id);

            BookInShoppingCart bs = new BookInShoppingCart();

            if (book != null)
            {
                bs.BookId = book.Id;
            }

            return View(bs);
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(BookInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingConfirmed(model, userId);



            return View("Index", _bookService.GetAll());
        }

        public IActionResult ExportToPdf()
        {
            var orders = _orderService.GetOrders();
            byte[] pdfBytes = _pdfService.GenerateOrdersPdf(orders);

            return File(pdfBytes, "application/pdf", "OrdersReport.pdf");
        }
    }
}

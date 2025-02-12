using Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class Order : BaseEntity
    {
        public string? userId { get; set; }
        public IntegratedSystemsUser? Owner { get; set; }
        public IEnumerable<BookInOrder>? BooksInOrder { get; set; }
    }
}

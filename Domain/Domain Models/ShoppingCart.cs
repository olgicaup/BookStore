using Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public IntegratedSystemsUser? Owner { get; set; }
        public virtual ICollection<BookInShoppingCart>? BookInShoppingCarts { get; set; }
    }
}

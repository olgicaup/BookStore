using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models
{
    public class Publisher : BaseEntity
    {
        public Guid Id { get; set; }
        public string? PublisherName { get; set; }
        public virtual ICollection<Book>? Books { get; set; }
    }
}

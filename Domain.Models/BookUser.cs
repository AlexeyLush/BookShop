using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class BookUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}

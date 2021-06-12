using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User: IdentityUser
    {
        public UserType UserType { get; set; }
        public IList<Book> Books { get; set; } = new List<Book>();
        public int Cash { get; set; } = 5000;
    }
}

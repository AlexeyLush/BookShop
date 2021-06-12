using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Genre
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Назавние")]
        public string Title { get; set; }
    }
}

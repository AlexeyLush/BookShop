using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Назавние")]
        public string Title { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Жанр")]
        public Guid GenreId { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }

        [Display(Name = "Обложка")]
        public byte[] Photo { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        
        [Display(Name = "Цена")]
        public int Price { get; set; }

    }
}

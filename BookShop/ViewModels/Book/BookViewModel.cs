#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BookShop.ViewModels.Book
{
    public class BookViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите название")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите имя автора")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Выберите жанр")]
        [Display(Name = "Жанр")]
        public Guid GenreId { get; set; }

        [Required(ErrorMessage = "Введите год")]
        [Display(Name = "Год")]
        [Range(1000, 2021, ErrorMessage = "Год должен находится в диапазоне от 1000 - 2021")]
        public int Year { get; set; }

        [Display(Name = "Обложка")]
        public IFormFile? Photo { get; set; }

        [Required(ErrorMessage = "Заполните описание для книги")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Заполните цену")]
        [Range(2000, 75000, ErrorMessage = "Цена должна находится в диапазоне от 2000 - 75000")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

    }
}

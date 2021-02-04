using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_ASP.Models
{
    [Table("Bookshelf")]
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Data wydania")]
        [DisplayFormat(DataFormatString = "{0:yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Tytuł musi posiadać co najmniej dwa znaki!")]
        [MaxLength(200, ErrorMessage = "Tytuł nie może przekraczać dwustu znaków!")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Pole Autor musi posiadać co najmniej dwa znaki!")]
        [MaxLength(200, ErrorMessage = "Pole Autor nie może przekraczać dwustu znaków!")]
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Opis musi posiadać co najmniej dwa znaki!")]
        [MaxLength(2000, ErrorMessage = "Opis nie może przekraczać dwóch tysięcy znaków!")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [MinLength(2, ErrorMessage = "Ścieżka musi posiadać co najmniej dwia znaki!")]
        [MaxLength(200, ErrorMessage = "Ścieżka nie może przekraczać dwustu znaków!")]
        [Display(Name = "Ścieżka do zdjęcia")]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [Display(Name = "Przeczytane")]
        public bool IsRead { get; set; }
    }
}

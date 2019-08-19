using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proj.Models;
using System.ComponentModel.DataAnnotations;

namespace Proj.ViewModels
{
    public class NewMoviewViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please, enter movie Name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please, select Genre from the list")]
        public byte? GenreId { get; set; }

        [Display(Name = "Date of Release")]
        [Required(ErrorMessage = "Date Format: dd.mm.yyyy")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 20, ErrorMessage = "Quantity must be higher than 0 and less than 21.")]
        public int? NumberInStock { get; set; }

        public string ViewForm
        {
            get
            {
                return Id != 0 ? "Update" : "FormToCreateMovie";
            }
        }

        public NewMoviewViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }

        public NewMoviewViewModel()
        {
            Id = 0;
        }
    }
}
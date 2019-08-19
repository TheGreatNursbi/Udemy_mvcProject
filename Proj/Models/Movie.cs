using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proj.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please, enter movie Name")]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage ="Please, select Genre from the list")]
        public byte GenreId { get; set; }

        [Display(Name = "Date of Release")]
        [Required(ErrorMessage ="Date Format: dd.mm.yyyy")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage ="Quantity is required")]
        [Range(0, 20, ErrorMessage = "Quantity must be higher than 0 and less than 21.")]
        public int NumberInStock { get; set; }
    }
}
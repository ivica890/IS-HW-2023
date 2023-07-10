using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApplication.Models.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1950, 2100)]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Ticket Price")]
        [Range(1, 500)]
        public double TicketPrice { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}

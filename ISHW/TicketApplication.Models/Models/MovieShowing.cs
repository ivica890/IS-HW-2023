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
    public class MovieShowing
    {
        [Key]
        public int Id { get; set; }

        public int AvailableSeats { get; set; }
       
        public int MovieId {get; set; }

        [ForeignKey("MovieId")]
        [ValidateNever]
        public Movie Movie { get; set; }

        public int CinemaHallId { get; set; }

        [ForeignKey("CinemaHallId")]
        [ValidateNever]
        public CinemaHall CinemaHall { get; set; }

        public DateTime StartTime { get; set; }


    }
}

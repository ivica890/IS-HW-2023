using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Models;

namespace TicketApplication.Models.Relationship
{
    public class ShowingInOrder
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        [ValidateNever]
        public Order Order { get; set; }

        public int MovieShowingId { get; set; }

        [ForeignKey("MovieShowingId")]
        [ValidateNever]
        public MovieShowing MovieShowing { get; set; }
    }
}

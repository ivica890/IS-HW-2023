using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Models;

namespace TicketApplication.Models.Relationship
{
    public class ShowingInShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingCartId { get; set;}

        [ForeignKey("ShoppingCartId")]
        [ValidateNever]
        public ShoppingCart ShoppingCart { get; set;}

        public int MovieShowingId { get; set; }

        [ForeignKey("MovieShowingId")]
        [ValidateNever]
        public MovieShowing MovieShowing { get; set; }

        public int Quantity { get; set; }
    }
}

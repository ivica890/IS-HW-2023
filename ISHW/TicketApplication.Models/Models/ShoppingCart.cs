using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Relationship;

namespace TicketApplication.Models.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public ICollection<ShowingInShoppingCart> showingsInShoppingCarts { get; set; }  

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        public int totalSum { get; set; }
    }
}

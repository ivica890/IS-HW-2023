using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Relationship;

namespace TicketApplication.Models.Models
{
    public class Order
    {

        public int Id { get; set; }

        public string userId { get; set; }


        [ForeignKey("userId")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }

        public DateTime orderDate { get; set; }


        public ICollection<ShowingInOrder> showingsInOrder { get; set; }
    
        public int totalSum { get; set; }
    }
}

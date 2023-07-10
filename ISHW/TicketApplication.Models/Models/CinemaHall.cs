using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApplication.Models.Models
{
    public class CinemaHall
    {
        [Key]
        public int Id { get; set; } 

        public int Capacity { get; set; }

        public string Name { get; set; }


    }
}

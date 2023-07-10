using System.Web.Mvc;
using TicketApplication.Models.Models;

namespace TicketApplication.Models.ViewModels
{
    public class MovieVM
    {

        public Movie Movie { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    
    }
}

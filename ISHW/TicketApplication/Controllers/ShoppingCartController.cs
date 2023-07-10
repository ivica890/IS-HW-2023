using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Security.Claims;
using TicketApplication.Models.Models;
using TicketApplication.Models.Relationship;
using TicketApplication.Services.Interface;

namespace TicketApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMovieShowingService _movieShowingService;

        public ShoppingCartController(IShoppingCartService service, IMovieShowingService movieShowingService)
        {
            _shoppingCartService = service;
            _movieShowingService = movieShowingService;

        }

        public IActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCart cart = _shoppingCartService.Get(x => x.UserId == userId);
            return View(cart);
        }



        public IActionResult AddToCart(int id) {

            MovieShowing movieShowing = _movieShowingService.Get(x => x.Id == id);

            ShowingInShoppingCart showingInCart = new()
            {
                MovieShowing = movieShowing,
                MovieShowingId = id,
                Quantity = 0
            };
            return View(showingInCart);
        
        }

        [HttpPost,ActionName("AddToCart")]
        [Authorize]
        public IActionResult AddToCartPost(ShowingInShoppingCart showingInShoppingCart)
        {

            if (ModelState.IsValid)
            {
                MovieShowing movieShowing = _movieShowingService.Get(x => x.Id == showingInShoppingCart.MovieShowingId);

                int number_of_tickets = showingInShoppingCart.Quantity;

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            
                _shoppingCartService.AddMovieShowingToShoppingCart(userId,movieShowing, number_of_tickets);

                return RedirectToAction("Index");
            }

            return View(showingInShoppingCart);
        }
    }
}

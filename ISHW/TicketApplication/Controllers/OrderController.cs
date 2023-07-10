using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;
using TicketApplication.Models.Models;
using TicketApplication.Services.Interface;

namespace TicketApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {

            List<Order>? orders = _orderService.GetAll().ToList();

            return View(orders);
        }

        public IActionResult GetAllForUser()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<Order>? orders = _orderService.GetAllForUser(userId).ToList();

            ViewBag.totalSum = _orderService.GetTotalSumForUser(userId);

            return View(orders);
        }


        public IActionResult Confirm() {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool orderCompleted = _orderService.CompleteOrder(userId);

            if (orderCompleted) {

                TempData["message"] = "Order completed successfully!";
                return RedirectToAction("Index", "Home");
            }

            TempData["message"] = "Order completed successfully!";
            return RedirectToAction("Index", "Home");
        }
  
    }
}

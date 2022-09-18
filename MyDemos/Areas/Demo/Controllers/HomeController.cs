using Microsoft.AspNetCore.Mvc;
using MyDemos.Areas.Demo.ViewModels;

namespace MyDemos.Areas.Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Hello world");               // HTTP Response 200 "Ok"
        }

        public IActionResult Index2()
        {
            return View();              
        }

        // HTTP GET
        public IActionResult DisplayCustomer()
        {
            CustomerViewModel viewModel = new CustomerViewModel()
            {
                CustomerId = 1,
                CreatedOn = System.DateTime.Now
            };
            return View(viewModel);
        }

        // To address over-posting ensure that the [Bind] attribute lists all the needed properties.
        // NOTE: the names of the properties is CASE-SENSITIVE.
        // HTTP POST
        [HttpPost]
        [ValidateAntiForgeryToken]                      // to address JavaScript Injection Attacks
        public IActionResult DisplayCustomer(
            [Bind("CustomerId,CustomerName,Email,Balance")] CustomerViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}

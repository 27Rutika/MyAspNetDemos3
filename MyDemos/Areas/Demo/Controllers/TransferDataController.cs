using Microsoft.AspNetCore.Mvc;
using MyDemos.Areas.Demo.ViewModels;

namespace MyDemos.Areas.Demo.Controllers
{
    /// <summary>
    ///     Demos showing how to transfer data from Controller to the View
    ///     (a) ViewBag
    ///     (b) ViewData
    ///     (c) TempData
    ///     (d) ViewModel
    /// </summary>

    [Area("Demo")]
    public class TransferDataController : Controller
    {
        /// <summary>
        ///     Demo showing how to transfer data from Controller to the View using ViewBag 
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewBagDemo()
        {
            ViewBag.Name = "Hello world - using ViewBag";
            ViewBag.SomeData = new string[] { "one", "two", "three" };
            return View();
        }

        /// <summary>
        ///     Demo showing how to transfer data from Controller to the View using ViewData
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewDataDemo()
        {
            ViewData["Name"] = "Hello world - using ViewData";
            ViewData["SomeData"] = new string[] { "one", "two", "three" };
            return View();
        }


        /// <summary>
        ///     Demo showing how to transfer data 
        ///     from Controller Action Method to another Action Method OR View using TempData
        ///     in the same Request/Response Pipeline
        /// </summary>
        /// <returns></returns>
        public IActionResult TempDataDemoStep1()
        {
            ViewBag.Name = "Manoj Kumar Sharma";
            ViewData["Company"] = "Birla Soft";

            TempData["Country"] = "hello world - from TempData";

            return RedirectToAction("TempDataDemoStep2");
        }

        public IActionResult TempDataDemoStep2()
        {
            string name = ViewBag.Name;
            string company = ViewData["Company"] as string;         // unboxing
            string country = string.Empty;

            if (TempData["Country"] != null)
            {
                country = TempData["Country"] as string;                    // unboxing
                TempData["Country"] = country + "-- changed in Step 2";
            }
            return RedirectToAction("TempDataDemoStep3");
        }

        public IActionResult TempDataDemoStep3()
        {
            string country = string.Empty;

            if (TempData["Country"] != null)
            {
                country = TempData["Country"] as string;
                TempData["Country"] = country + "-- changed in Step 3";
            }

            return View(viewName: "TempDataDemo");
        }

        /// <summary>
        ///     Demo showing how to transfer data from Controller to the View using ViewModel object
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewModelDemo()
        {
            CustomerViewModel viewModel = new CustomerViewModel()
            {
                CustomerId = 1,
                CreatedOn = System.DateTime.Now
            };
            return View(viewModel);
        }

    }
}
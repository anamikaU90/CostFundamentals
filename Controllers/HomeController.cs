using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CostPrice.Models;

namespace CostPrice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CostModel share)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string type = Request.Form["Category"].ToString();
                    type = type == "" ? "FIFO" : type;
                    shareResponse obj = Calculation.FIFO(share, type);
                    if (obj.cp == 0)
                        ViewBag.Name = "Error : Shares Sold is greater than Purchased can be a reason";
                    else
                    ViewBag.Name = string.Format("<b>Cost Price of Sold Shares: </b> {0} <br/>  <b>Gain Loss on sales : </b>{1} <br/> <b> No. of Remaining Shares : </b>{2} <br/> <b> Cost Price of Remaining Shares: </b>{3}", obj.cp, obj.gain, obj.sharesRem, obj.cpRem);
                }
            }
            catch(Exception ex) { }
            return View();
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

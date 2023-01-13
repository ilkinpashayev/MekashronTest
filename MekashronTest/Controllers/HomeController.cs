using MekashronService;
using MekashronTest.Models;
using MekashronTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace MekashronTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMekashronClient _mekashronClient;

        public HomeController(ILogger<HomeController> logger,IMekashronClient mekashronClient)
        {
            _logger = logger;
            _mekashronClient = mekashronClient;
        }

        public IActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            MekashronRequestResult mekashronRequestResult = _mekashronClient.Login(username, password);   
            if (mekashronRequestResult.ResultCode== -1) {
                TempData["alertMessage"] = mekashronRequestResult.ResultMessage;
            }
            else
            {                
                TempData["successMessage"] = "Success";
                
            }
            return RedirectToAction("index");
        }

    }
}
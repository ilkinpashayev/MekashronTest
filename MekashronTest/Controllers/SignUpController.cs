using MekashronTest.Models;
using MekashronTest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace MekashronTest.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ILogger<SignUpController> _logger;
        private readonly IMekashronClient _mekashronClient;

        public SignUpController(ILogger<SignUpController> logger, IMekashronClient mekashronClient)
        {
            _logger = logger;
            _mekashronClient = mekashronClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup([FromForm] Register newCustomer)
        {
            MekashronService.RegisterNewCustomerRequest registerNewCustomerRequest = new MekashronService.RegisterNewCustomerRequest()
            {
                CountryID = newCustomer.CountryID,
                Email = newCustomer.Email,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Mobile = newCustomer.Mobile,
                Password = newCustomer.Password
            };

            MekashronRequestResult mekashronRequestResult = _mekashronClient.Register(registerNewCustomerRequest);
            if (mekashronRequestResult.ResultCode == -1)
            {
                TempData["alertMessage"] = mekashronRequestResult.ResultMessage ;
            }
            else
                TempData["successMessage"] = "Success";
            return RedirectToAction("index");

        }
    }
}

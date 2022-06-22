using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Models;
using NorbitsChallenge.Helpers;

namespace NorbitsChallenge.Controllers
{
    public class SettingsController: Controller
    {
        private readonly IConfiguration _config;

        public SettingsController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Function for changing logged on user variable in user helper, thus changing account.
        public IActionResult ChangeCompany(int CompanyId)
        {
            UserHelper.CompanyId = CompanyId;
            
            return RedirectToAction("Index", "Home"/*, new { area = "" }*/);
        }
    }
}

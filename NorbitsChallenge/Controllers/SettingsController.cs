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
        public IActionResult ChangeCompany(int companyId)
        {
            UserHelper.CompanyId = companyId;
            
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public IActionResult Update( int CompanyId)
        {
            return RedirectToAction("ChangeCompany", new {companyId = CompanyId});
        }
    }
}

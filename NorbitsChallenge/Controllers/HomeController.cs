﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var model = GetCompanyModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Index(int companyId, string licensePlate)
        {
            var tireCount = new CarDb(_config).GetTireCount(companyId, licensePlate);

            var model = GetCompanyModel();
            model.TireCount = tireCount;

            return Json(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Creating a List of all cars to present.
        public IActionResult ListAllCars()
        {
            List<Car> cars = new CarDb(_config).GetAllCars();
            return View("ListAllCars", cars);
        }

        //Add a new car
        public IActionResult Create()
        {
            return View();
        }

        //Add a new Car From Form in Create.cshtml
        [HttpPost]
        public IActionResult NewCar([FromForm] Car car)
        {
            new CarDb(_config).AddCarToDb(car);
            return RedirectToAction("ListAllCars");
        }

        //Remove a Car From the List.
        public IActionResult Delete(string id)
        {
            Car car = new CarDb(_config).GetCar(id);
            return View(car);
        }

        //DeleteCar to remove after confirming deletion on delete page
        public IActionResult DeleteCar(string Licenseplate)
        {
            new CarDb(_config).RemoveCarFromDb(Licenseplate);
            return RedirectToAction("Index");
        }



        //Error Method With No Cache.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Make The Company Model
        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            var CarsInShopList = new CarDb(_config).GetAllCarsCompany(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName, CarsInShop = CarsInShopList };
        }
    }
}

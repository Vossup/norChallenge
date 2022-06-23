using System;
using System.Collections;
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

        //------ Base Case Pages-------//
        public IActionResult Index(string SearchString)
        {
            var model = GetCompanyModel();

            //filtering list of cars based on SearchString from form, based on licenseplates.
            if(model.CarsInShop.Count != 0 && !String.IsNullOrEmpty(SearchString)){
                var filteredCars = from car in model.CarsInShop select car;
                model.CarsInShop = filteredCars.Where(car => car.LicensePlate.Contains(SearchString)).ToList();
            };

            return View(model);
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
        //------ Base Case Pages End-------//



        //Creating a List of all cars to present.//
        public IActionResult ListAllCars()
        {
            List<Car> cars = new CarDb(_config).GetAllCars();
            return View("ListAllCars", cars);
        }

        //Add a new car//
        public IActionResult Create()
        {
            return View();
        }

        //Add a new Car From Form in Create.cshtml//
        [HttpPost]
        public IActionResult NewCar([FromForm] Car car)
        {
            var lc = car.LicensePlate;
            new CarDb(_config).AddCarToDb(car);
            return RedirectToAction("ListAllCars");
        }

        //Remove a Car From the List.
        public IActionResult Delete(string id)
        {
            Car car = new CarDb(_config).GetCar(id);
            return View(car);
        }

        //DeleteCar to remove after confirming deletion on delete page//
        public IActionResult DeleteCar(string Licenseplate)
        {
            new CarDb(_config).RemoveCarFromDb(Licenseplate);
            return RedirectToAction("Index");
        }

        //Go To Edit Car Page//
        public IActionResult Edit(string id)
        {
            Car car = new CarDb(_config).GetCar(id);
            return View(car);
        }

        //Edit Car Function//
        [HttpPost]
        public IActionResult EditCar([FromForm] Car car)
        {
            new CarDb(_config).UpdateCarDb(car);
            return RedirectToAction("Index");
        }


        //Error Method With No Cache.//
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Make The Company Model//
        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            var CarsInShopList = new CarDb(_config).GetAllCarsCompany(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName, CarsInShop = CarsInShopList };
        }
    }
}

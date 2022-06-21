using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Models
{
    public class Car
    {
        public Car()
        {
                
        }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int TireCount { get; set; }
        public int CompanyId { get; set; }
    }
}

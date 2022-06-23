using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        //setting regex rules for creation of license plates.
        [Required(ErrorMessage = "License Plate Can not be empty", AllowEmptyStrings = false)]
        [RegularExpression(@"/^[a-zA-Z0-9_.-]*$/gm", ErrorMessage = "License Plate can not contain special characters")]
        public string LicensePlate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public int TireCount { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}

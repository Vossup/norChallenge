using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }

        public int GetTireCount(int companyId, string licensePlate)
        {
            int result = 0;

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand {Connection = connection, CommandType = CommandType.Text})
                {
                    command.CommandText = $"select * from car where companyId = {companyId} and licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = (int) reader["tireCount"];
                        }
                    }
                }
            }

            return result;
        }

        //Get Single Car.
        public Car GetCar(string licensePlate)
        {
            Car result = new Car();

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where licenseplate = '{licensePlate}'";

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.LicensePlate = (string)reader["LicensePlate"];
                            result.Description = (string)reader["Description"];
                            result.Model = (string)reader["Model"];
                            result.Brand = (string)reader["Brand"];
                            result.TireCount = (int)reader["TireCount"];
                            result.CompanyId = (int)reader["CompanyId"];
                        }
                    }
                }
            }
            return result;
        }

        //Get All Cars Tied To One Workshop
        public List<Car> GetAllCarsCompany(int companyId)
        {
            List<Car> result = new List<Car>();

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from car where companyId = {companyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car carTmp = new Car();
                            carTmp.LicensePlate = (string)reader["LicensePlate"];
                            carTmp.Description = (string)reader["Description"];
                            carTmp.Model = (string)reader["Model"];
                            carTmp.Brand = (string)reader["Brand"];
                            carTmp.TireCount = (int)reader["TireCount"];
                            carTmp.CompanyId = (int)reader["CompanyId"];
                            result.Add(carTmp);
                        }
                    }
                }
            }
            return result;
        }

        //Get All Cars Registered
        public List<Car> GetAllCars()
        {
            List<Car> result = new List<Car>();

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = "select * from car";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car carTmp = new Car();
                            carTmp.LicensePlate = (string)reader["LicensePlate"];
                            carTmp.Description = (string)reader["Description"];
                            carTmp.Model = (string)reader["Model"];
                            carTmp.Brand = (string)reader["Brand"];
                            carTmp.TireCount = (int)reader["TireCount"];
                            carTmp.CompanyId = (int)reader["CompanyId"];
                            result.Add(carTmp);
                        }
                    }
                }
            }
            return result;
        }

        //Add a Car
        public void AddCarToDb(Car car)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"insert into Car values ('{car.LicensePlate}', '{car.Description}', '{car.Model}', '{car.Brand}', {car.TireCount}, {car.CompanyId})";
                    command.ExecuteNonQuery();
                }
            }
        }

        //Remove a Car
        public void RemoveCarFromDb(string LicensePlate)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using( var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"Delete from Car where LicensePlate = '{LicensePlate}'";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

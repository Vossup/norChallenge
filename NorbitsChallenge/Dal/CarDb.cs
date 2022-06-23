using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Models;
using NorbitsChallenge.Helpers;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
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
                    command.Parameters.AddWithValue("@LicensePlate", $"{licensePlate}");

                    command.CommandText = $"select * from car where licenseplate = @LicensePlate";

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
                    command.Parameters.AddWithValue("@companyId", companyId);
                    command.CommandText = $"select * from car where companyId = @companyId";

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
                    command.Parameters.AddWithValue("@LicensePlate", $"{car.LicensePlate}");
                    command.Parameters.AddWithValue("@Description", $"{car.Description}");
                    command.Parameters.AddWithValue("@Model", $"{car.Model}");
                    command.Parameters.AddWithValue("@Brand", $"{car.Brand}");
                    command.Parameters.AddWithValue("@TireCount", car.TireCount);
                    command.Parameters.AddWithValue("@CompanyId", car.CompanyId);

                    command.CommandText = $"insert into Car values (@LicensePlate, @Description, @Model, @Brand, @TireCount, @CompanyId)";
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
                    command.Parameters.AddWithValue("@LicensePlate", $"{LicensePlate}");

                    command.CommandText = $"Delete from Car where LicensePlate = @LicensePlate";
                    command.ExecuteNonQuery();
                }
            }
        }

        //Update Car Information
        public void UpdateCarDb(Car car)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.Parameters.AddWithValue("@Description", $"{car.Description}");
                    command.Parameters.AddWithValue("Model", $"{car.Model}");
                    command.Parameters.AddWithValue("Brand", $"{car.Brand}");
                    command.Parameters.AddWithValue("TireCount", car.TireCount);
                    command.Parameters.AddWithValue("CompanyId", car.CompanyId);
                    command.Parameters.AddWithValue("@LicensePlate", $"{car.LicensePlate}");

                    command.CommandText =
                        $"Update Car " +
                        $"set Description = @Description," +
                        $"Model = @Model," +
                        $"Brand = @Brand," +
                        $"TireCount = @TireCount," +
                        $"CompanyId = @CompanyId " +
                        $"where LicensePlate = @LicensePlate";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

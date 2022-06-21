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
    }
}

using Application.Repositories;
using Core.Application.Repositories;
using Domain;
using Domain.Entities;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IRedisRepository _redis;
        private readonly IAsyncRepository<User> _user;
        private readonly IAsyncRepository<Order> _order;

        public ExcelService(IRedisRepository redis, IAsyncRepository<User> user
            , IAsyncRepository<Order> order)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
            _order = order ?? throw new ArgumentNullException(nameof(order));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            if(file == null)
                throw new NotFoundException(file.FileName);
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            if (!extension.Equals(".xlsx") && !extension.Equals(".xls"))
            {
                throw new FileTypeException();
            }
            string fileName = DateTime.Now.Ticks + extension;
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
            if (!Directory.Exists(pathBuilt))
            {
                Directory.CreateDirectory(pathBuilt);
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
               fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return (pathBuilt + "\\" + fileName);
        }

        public async Task AddAllUser(string fileName)
        {
            List<User> users = new List<User>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    var dataSet = reader.AsDataSet(conf);
                    var dataTable = dataSet.Tables[0];

                    while (reader.Read())
                    {
                        if (reader.GetValue(0).ToString().Contains("Id"))
                            throw new ValidationException();
                        var newUser = new User
                        {
                            UserId = reader.GetDouble(0),
                            FirstName = reader.GetValue(1).ToString(),
                            LastName = reader.GetValue(2).ToString(),
                            Gender = reader.GetValue(3).ToString(),
                            Country = reader.GetValue(4).ToString(),
                            Age = reader.GetDouble(5),
                            Date = reader.GetValue(6).ToString()
                        };
                        await _redis.AddUser(newUser);
                        users.Add(newUser);

                    }
                }
                await _user.AddRange(users);
            }

        }
        public async Task AddAllOrder(string fileName)
        {
            List<Order> orders = new List<Order>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    var dataSet = reader.AsDataSet(conf);
                    var dataTable = dataSet.Tables[0];

                    while (reader.Read())
                    {

                        if (reader.GetValue(0).ToString().Contains("Id"))
                            throw new ValidationException();
                        var newOrder = new Order
                        {
                            OrderID = reader.GetDouble(0),
                            OrderDate = reader.GetValue(1).ToString(),
                            CustomerID = reader.GetValue(2).ToString(),
                            CustomerName = reader.GetValue(3).ToString(),
                            Country = reader.GetValue(4).ToString(),
                            PostalCode = reader.GetValue(5).ToString(),
                            ProductID = reader.GetValue(6).ToString(),
                            Category = reader.GetValue(7).ToString(),
                            ProductName = reader.GetValue(8).ToString(),
                            Sales = reader.GetDouble(9),
                            Discount = reader.GetDouble(10),
                            Profit = reader.GetDouble(11)
                        };
                        await _redis.AddOrder(newOrder);
                        orders.Add(newOrder);
                    }
                }
                await _order.AddRange(orders);
            }
        }

    }
}

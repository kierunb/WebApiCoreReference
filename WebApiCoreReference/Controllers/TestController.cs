using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCoreReference.Database;
using WebApiCoreReference.Models;

namespace WebApiCoreReference.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly NorthwindContext db;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public TestController(NorthwindContext db, 
            IMapper mapper, 
            ILogger<TestController> logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("test1")]
        public IActionResult Test1()
        {

            //var result = db
            //    .Orders
            //    .Include(c => c.Customer)
            //    .ToList();

            var result = db.Orders.ProjectTo<Models.OrderDTO>(mapper.ConfigurationProvider);
            
            return Ok(result);
        }


        [HttpGet("test2")]
        public IActionResult Test2()
        {
            // jezeli pobieracie dane tylko po to zeby np.:
            // wysiwetlic je w widoku
            // .AsNoTracking() -> 

            var customer1 = db.Customers.FirstOrDefault();
            
            logger.LogInformation($"customer state: {db.Entry(customer1).State}");

            customer1.City = "Sosnowiec";

            logger.LogInformation($"customer state: {db.Entry(customer1).State}");

            var orders = db.Orders.AsNoTracking().ToList();

            return Ok(orders);
        }


        [HttpGet("test3")]
        public IActionResult Test3()
        {
            var orders = db.Orders.AsNoTracking().ToList();

            //var orders = (from o in db.Orders
            //              select new OrderDTO
            //              {
            //                  ShipName = o.ShipName.ToUpper(),
            //                  ShipCity = o.ShipCity,
            //                  ShipCountry = o.ShipCountry
            //              })
            //              .AsNoTracking().ToList();

            //var orders = db.Orders
            //    .AsNoTracking()
            //    .ProjectTo<OrderDTO>(mapper.ConfigurationProvider);

            return Ok(orders);
        }


        [HttpGet("test4")]
        public IActionResult Test4()
        {
            //1. ponieranie danych z kilku (2) tabel

            var orders = (from o in db.Orders
                         join c in db.Customers on o.CustomerId equals c.CustomerId
                         select new {
                            ShipName = o.ShipName,
                            Customer = c.ContactName
                         })
                         .AsNoTracking().ToList();

            return Ok(orders);
        }

        [HttpGet("test5")]
        public IActionResult Test5()
        {
            // w ef core domyslnie dziala tzw. eager loading
            //var order = db.Orders.FirstOrDefault();
            var order = db.Orders
                .Include(o => o.Customer)
                .FirstOrDefault();

            var customerName = order.Customer.ContactName;

            //var empl = order.Employee.Address;

            this.logger.LogInformation($"Customer name: {customerName}");

            return Ok(order.OrderId);
        }

        [HttpGet("test6")]
        public async Task<IActionResult> Test6()
        {
            // customer o jakims id i jego zamowienia
            // zmapowany na obiekt

            // Customer i jego Orders -> 1 do wielu

            var customerOrders = await db
                .Customers
                //.Where(c => c.Orders.Any(o => o.OrderId == 10643))
                .ProjectTo<CustomerOrdersDTO>(mapper.ConfigurationProvider)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == "ALFKI");

            //var customerOrders2 = db
            //    .Customers
            //    .Include(c => c.Orders);
            //    //.Select() // mapowanie 

            return Ok(customerOrders);
        }
    }
}

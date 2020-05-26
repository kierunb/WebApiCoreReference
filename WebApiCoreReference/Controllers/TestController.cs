using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCoreReference.Database;

namespace WebApiCoreReference.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly NorthwindContext db;
        private readonly IMapper mapper;

        public TestController(NorthwindContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
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
    }
}

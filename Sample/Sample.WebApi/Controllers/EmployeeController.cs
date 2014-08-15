using AttributeRouting;
using AttributeRouting.Web.Http;
using Hummingbird.Data;
using Sample.Employees.Data;
using Sample.Employees.Domain;
using Sample.Orders.Data;
using Sample.Orders.Domain;
using Sample.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sample.WebApi.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [GET(""), HttpGet]
        public IPagedResults<Employee> Search(string firstName, int pageSize, int pageNum)
        {
            var page = _employeeRepo.Search(firstName, pageSize, pageNum);

            return page;
        }

    }
}

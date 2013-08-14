﻿using AttributeRouting;
using AttributeRouting.Web.Http;
using Northwind.Orders.Data;
using Northwind.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hummingbird.WebApi.Controllers
{
    [RoutePrefix("api/1/customers")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [GET("")]
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepo.GetTop(100);
        }

    }
}

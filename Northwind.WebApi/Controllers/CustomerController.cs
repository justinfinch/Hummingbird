using AttributeRouting;
using AttributeRouting.Web.Http;
using Northwind.Orders.Data;
using Northwind.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Northwind.WebApi.Controllers
{
    [RoutePrefix("api/1/customers")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IOrderRepository _orderRepo;

        public CustomerController(ICustomerRepository customerRepo, IOrderRepository orderRepo)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
        }

        [GET("")]
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepo.GetTop(100);
        }

        [GET("favorites")]
        public IEnumerable<Customer> GetFavorites()
        {
            return _customerRepo.GetFavoriteCustomers();
        }

    }
}

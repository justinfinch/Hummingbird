using AttributeRouting;
using AttributeRouting.Web.Http;
using Hummingbird.Data;
using Sample.Orders.Data;
using Sample.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sample.WebApi.Controllers
{
    [RoutePrefix("api/1/customers")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IUnitOfWorkFactory _uowFactory;

        public CustomerController(ICustomerRepository customerRepo, IOrderRepository orderRepo, IUnitOfWorkFactory uowFactory)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
            _uowFactory = uowFactory;
        }
        //TODO: Fix to not use domain objects

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

        [PUT("")]
        public Customer Update(Customer request)
        {
            using (var unitOfWork = _uowFactory.Create())
            {
                
                unitOfWork.Commit();
            }

            return null;
        }
    }
}

using AttributeRouting;
using AttributeRouting.Web.Http;
using Hummingbird.Data;
using Sample.Employees.Data;
using Sample.Orders.Data;
using Sample.Orders.Domain;
using Sample.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sample.WebApi.Controllers
{
    [RoutePrefix("api/1/orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUnitOfWorkFactory _uowFactory;
        private readonly IEmployeeRepository _employeeRepo;

        public OrderController(IOrderRepository orderRepo, 
            IEmployeeRepository employeeRepo, 
            IUnitOfWorkFactory uowFactory)
        {
            _orderRepo = orderRepo;
            _employeeRepo = employeeRepo;
            _uowFactory = uowFactory;
        }

        [POST("")]
        public Order PlaceOrder(OrderDto order) //TODO: SHould be a dto
        {
            Order newOrder = null;
            using (var uow = _uowFactory.Create())
            {
                newOrder = new Order(DateTime.Now, order.Total, order.EmployeeNumber);
                var employee = _employeeRepo.GetByNumber(newOrder.EmployeeNumber);
                employee.IncrementTotalSales(1);

                _orderRepo.Save(newOrder);
                _employeeRepo.Save(employee);

                uow.Commit();
            }

            return newOrder;
        }

    }
}

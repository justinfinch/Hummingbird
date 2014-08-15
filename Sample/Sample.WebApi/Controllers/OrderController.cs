using AttributeRouting;
using AttributeRouting.Web.Http;
using Hummingbird.Data;
using Sample.Employees.Data;
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
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUnitOfWorkFactory _uowFactory;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IDynamicDataProvider _dynamicDataProvider;

        public OrderController(IOrderRepository orderRepo, 
            IEmployeeRepository employeeRepo, 
            IUnitOfWorkFactory uowFactory,
            IDynamicDataProvider dynamicDataProvider)
        {
            _orderRepo = orderRepo;
            _employeeRepo = employeeRepo;
            _uowFactory = uowFactory;
            _dynamicDataProvider = dynamicDataProvider;
        }

        [GET(""), HttpGet]
        public IEnumerable<Order> Search()
        {
            //return _orderRepo.Search(DateTime.Today, 100M);
            return _orderRepo.Search(new DateTime(2013, 10, 12), 100M);
        }

        [GET("withsproc"), HttpGet]
        public IEnumerable<OrderDto> UseDynaicSproc()
        {
            //DON"T DO THIS IN THE CONTROLLER, JUST AN EXAMPLE
            dynamic results =_dynamicDataProvider.ExecuteSproc("OrdersGet", new
            {
                Total = 200M
            });

            var response = new Collection<OrderDto>();

            if (results != null)
            {
                foreach (dynamic result in results)
                {
                    var orderDto = new OrderDto();
                    orderDto.EmployeeNumber = result.EmpNum;
                    orderDto.Total = result.Total;
                    response.Add(orderDto);
                }
            }

            return response;
        }

        [POST("")]
        public Order PlaceOrder(OrderDto order) //TODO: SHould be a dto
        {
            Order newOrder = null;
            using (var uow = _uowFactory.Create())
            {
                newOrder = new Order(DateTime.Today, order.Total, order.EmployeeNumber);
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

using AutoMapper;
using Nancy;
using Northwind.Api.Dto;
using Northwind.Orders.Data;
using Northwind.Orders.Domain;

namespace Northwind.Api.Modules
{
    public class CustomerModule : NancyModule
    {

        public CustomerModule(ICustomerRepository customerRepository)
            :base("/api/v1/customers")
        {
            Get["/{Id}"] = p =>
                {
                    Response response = HttpStatusCode.NotFound;

                    Customer customer = customerRepository.Get(p.Id);
                    if (customer != null)
                    {
                        var customerDto = Mapper.Map<CustomerDto>(customer);
                        response = Response.AsJson(customerDto);
                    }

                    return response;
                };
        }

    }
}
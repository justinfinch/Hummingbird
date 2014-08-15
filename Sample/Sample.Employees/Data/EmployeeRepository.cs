using Hummingbird.Data;
using Sample.Employees.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Employees.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IQueryableDataProvider<Employee> _employeeDataProvider;

        public EmployeeRepository(IQueryableDataProvider<Employee> employeeDataProvider)
        {
            _employeeDataProvider = employeeDataProvider;
        }

        public Employee Save(Employee employee)
        {
            return _employeeDataProvider.Save(employee);
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetByNumber(int number)
        {
            return _employeeDataProvider.Find(e => e.Number == number).FirstOrDefault();
        }


        public IPagedResults<Employee> Search(string firstName,  int pageSize, int pageIndex)
        {
            var pagedRequest = new PagedRequest<Employee, string>()
                .Query(e => e.FirstName.StartsWith(firstName))
                .Page(pageIndex)
                .WithSize(pageSize)
                .OrderBy(e => e.LastName);

            return _employeeDataProvider.FindPage(pagedRequest);
        }
    }
}

using Sample.Employees.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Employees.Data
{
    public interface IEmployeeRepository
    {
        Employee Save(Employee employee);
        IEnumerable<Employee> GetAll();
        Employee GetByNumber(int number);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Honda.DAL.Models;

namespace Company.Honda.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll();
        public Employee? Get(int id);
        public int Add(Employee employee);
        public int Update(Employee employee);
        public int Delete(Employee employee);
    }
}

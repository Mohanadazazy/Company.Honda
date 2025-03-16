using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Honda.BLL.Interfaces;
using Company.Honda.DAL.Data.Contexts;
using Company.Honda.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.Honda.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDbContext _context;
        public EmployeeRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Employee> GetByName(string name)
        {
            return _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}

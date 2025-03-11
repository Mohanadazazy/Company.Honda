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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _context;
        public EmployeeRepository(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }
        public Employee? Get(int id)
        {
            return _context.Employees.Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public int Add(Employee employee)
        {
            _context.Employees.Add(employee);
            return _context.SaveChanges();
        }
        public int Update(Employee employee)
        {
            _context.Employees.Update(employee);
            return _context.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            return _context.SaveChanges();
        }

        

        
    }
}

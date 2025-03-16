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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;
        public GenericRepository(CompanyDbContext dbContext)
        {
            _context = dbContext;
        }
        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _context.Employees.Include(E => E.Department).ToList();
            }
            return _context.Set<T>().ToList();
        }

        public int Add(T employee)
        {
            _context.Set<T>().Add(employee);
            return _context.SaveChanges();
        }
        public int Update(T employee)
        {
            _context.Set<T>().Update(employee);
            return _context.SaveChanges();
        }

        public int Delete(T employee)
        {
            _context.Set<T>().Remove(employee);
            return _context.SaveChanges();
        }
    }
}

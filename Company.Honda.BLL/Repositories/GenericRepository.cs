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
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as T;
            }
            return _context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.Department).ToListAsync();
            }
            return _context.Set<T>().ToList();
        }

        public async Task AddAsync(T employee)
        {
            await _context.Set<T>().AddAsync(employee);
        }
        public void Update(T employee)
        {
            _context.Set<T>().Update(employee);
        }

        public void Delete(T employee)
        {
            _context.Set<T>().Remove(employee);
        }
    }
}

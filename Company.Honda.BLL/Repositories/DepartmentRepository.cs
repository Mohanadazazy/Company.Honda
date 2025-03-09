using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Honda.BLL.Interfaces;
using Company.Honda.DAL.Data.Contexts;
using Company.Honda.DAL.Models;

namespace Company.Honda.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context)
        {
            _context = context;
        }



        public IEnumerable<Department> GetAll()
        {
            return _context.Department.ToList();
        }

        public Department? Get(int id)
        {
            return _context.Department.Find(id);   
        }

        public int Add(Department department)
        {
            _context.Department.Add(department);
            return _context.SaveChanges();
        }

        public int Update(Department department)
        {
            _context.Department.Update(department);
            return _context.SaveChanges();
        }

        public int Delete(Department department)
        {
            _context.Department.Remove(department);
            return _context.SaveChanges();
        }
    }
}

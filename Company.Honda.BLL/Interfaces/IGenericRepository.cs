using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Honda.DAL.Models;

namespace Company.Honda.BLL.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetAsync(int id);
        public Task AddAsync(T model);
        public void Update(T model);
        public void Delete(T id);
    }
}

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
        public IEnumerable<T> GetAll();
        public T? Get(int id);
        public int Add(T department);
        public int Update(T department);
        public int Delete(T id);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Honda.DAL.Models;

namespace Company.Honda.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByNameAsync(string name);
    }
}

﻿using System;
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

        public EmployeeRepository(CompanyDbContext dbContext) : base(dbContext)
        {
            
        }



    }
}

using AutoMapper;
using Company.Honda.BLL.Interfaces;
using Company.Honda.BLL.Repositories;
using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.Honda.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _employeeRepository.GetAll();
            }
            else
            {
                employees = _employeeRepository.GetByName(SearchInput);
            }
                
            // Dictionary   : 3 Properties
            // 1. ViewData  : Transfer Extra Information From Controller To View
            ViewData["Message"] = "Hello From ViewData";
            // 2. ViewBag   : Transfer Extra Information From Controller To View
            //ViewBag.Message = "Hello Form ViewBag";

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentRepository.GetAll();
            ViewData["Departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //var Employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    Email = model.Email,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    DepartmentId = model.DepartmentId,
                //};
                var employee= _mapper.Map<Employee>(model);
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfuly";
                    return RedirectToAction(nameof(Index));
                }
                    
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();
            var model = _employeeRepository.Get(id.Value);
            if (model is null) return NotFound();
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["Departments"] = departments;
            if (id is null) return BadRequest();
            var model = _employeeRepository.Get(id.Value);
            if (model is null) return NotFound();
            //EmployeeDto employee = new EmployeeDto()
            //{
            //    Name = model.Name,
            //    Address = model.Address,
            //    Age = model.Age,
            //    CreateAt = model.CreateAt,
            //    Email = model.Email,
            //    HiringDate = model.HiringDate,
            //    IsActive = model.IsActive,
            //    IsDeleted = model.IsDeleted,
            //    Phone = model.Phone,
            //    Salary = model.Salary,
            //    Department = model.Department,
            //    DepartmentId = model.DepartmentId
            //};
            var employee = _mapper.Map<EmployeeDto>(model);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id,EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Id = id.Value,
                    Name = model.Name,
                    Address= model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    Email = model.Email,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    Department = model.Department,
                    DepartmentId = model.DepartmentId
                };
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee == null) return NotFound();
            _employeeRepository.Delete(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}

using Company.Honda.BLL.Interfaces;
using Company.Honda.BLL.Repositories;
using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Honda.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var Employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    Email = model.Email,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary
                };
                var count = _employeeRepository.Add(Employee);
                if (count > 0) 
                    return RedirectToAction(nameof(Index));
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
            if (id is null) return BadRequest();
            var model = _employeeRepository.Get(id.Value);
            if (model is null) return NotFound();
            EmployeeDto employee = new EmployeeDto()
            {
                Name = model.Name,
                Address = model.Address,
                Age = model.Age,
                CreateAt = model.CreateAt,
                Email = model.Email,
                HiringDate = model.HiringDate,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                Phone = model.Phone,
                Salary = model.Salary
            };
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
                    Salary = model.Salary
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

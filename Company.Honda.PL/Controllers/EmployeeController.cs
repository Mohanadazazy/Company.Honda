using AutoMapper;
using Company.Honda.BLL.Interfaces;
using Company.Honda.BLL.Repositories;
using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;
using Company.Honda.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.Honda.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private IEmployeeRepository _employeeRepository;
        //private IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
                
            // Dictionary   : 3 Properties
            // 1. ViewData  : Transfer Extra Information From Controller To View
            ViewData["Message"] = "Hello From ViewData";
            // 2. ViewBag   : Transfer Extra Information From Controller To View
            //ViewBag.Message = "Hello Form ViewBag";

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if(model.Image is not null)
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");
               
                var employee= _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee Created Successfuly";
                    return RedirectToAction(nameof(Index));
                }
                    
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();
            var model = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (model is null) return NotFound();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["Departments"] = departments;
            if (id is null) return BadRequest();
            var model = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (model is null) return NotFound();
            var employee = _mapper.Map<EmployeeDto>(model);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id,EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "Images");
                }
                if (model.Image is not null)
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "Images");


                var employee = _mapper.Map<Employee>(model);
                employee.Id = id.Value;
                _unitOfWork.EmployeeRepository.Update(employee);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee == null) return NotFound();
            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = await _unitOfWork.Complete();
            if (count > 0)
            {
                if(employee.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employee.ImageName, "Images");
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

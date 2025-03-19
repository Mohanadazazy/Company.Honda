using Company.Honda.BLL;
using Company.Honda.BLL.Interfaces;
using Company.Honda.BLL.Repositories;
using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Honda.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private IDepartmentRepository _departmentRepository;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet] // GET: /Department/Index
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDto model)
        {
            if(ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
                await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.Complete();
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();
            var Department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if(Department == null) return NotFound();
            return View(Department);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null) return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if(department == null) return NotFound();
            DepartmentDto model = new DepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
            return View(model);
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Id = id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department == null) return NotFound();
            _unitOfWork.DepartmentRepository.Delete(department);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}

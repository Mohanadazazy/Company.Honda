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
        private IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentDto model)
        {
            if(ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                };
                var count = _departmentRepository.Add(department);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest();
            var Department = _departmentRepository.Get(id.Value);
            if(Department == null) return NotFound();
            return View(Department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id is null) return BadRequest();
            var department = _departmentRepository.Get(id.Value);
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
        public IActionResult Edit([FromRoute]int id,DepartmentDto model)
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
                var count = _departmentRepository.Update(department);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var department = _departmentRepository.Get(id);
            if (department == null) return NotFound();
            var count = _departmentRepository.Delete(department);
            if(count > 0)
                return RedirectToAction(nameof(Index));
            return BadRequest();
        }
    }
}

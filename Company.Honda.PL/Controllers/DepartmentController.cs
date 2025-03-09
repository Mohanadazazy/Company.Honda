using Company.Honda.BLL.Interfaces;
using Company.Honda.BLL.Repositories;
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
    }
}

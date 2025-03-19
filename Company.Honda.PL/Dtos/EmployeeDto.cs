using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.Honda.DAL.Models;

namespace Company.Honda.PL.Dtos
{
    public class EmployeeDto
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        public int? Age { get; set; }
        [EmailAddress(ErrorMessage ="example@gmil.com")]
        public string Email { get; set; }
        [RegularExpression("[0-9]{0,3}-[a-zA-Z]{0,4}-[a-zA-Z]{0,3}-[a-zA-Z]{0,3}$")]
        public string Address { get; set; }
        [RegularExpression("01[0125][0-9]{8}$")]
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreateAt { get; set; }
        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
    }
}

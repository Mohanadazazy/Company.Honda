using System.ComponentModel.DataAnnotations;
using Company.Honda.DAL.Models;

namespace Company.Honda.PL.Dtos
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date Of Creation Is Required")]
        public DateTime CreateAt { get; set; }
        
    }
}

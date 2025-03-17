using AutoMapper;
using Company.Honda.DAL.Models;
using Company.Honda.PL.Dtos;

namespace Company.Honda.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto> ();
        }
    }
}

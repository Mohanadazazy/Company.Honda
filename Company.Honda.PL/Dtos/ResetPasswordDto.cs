using System.ComponentModel.DataAnnotations;

namespace Company.Honda.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Confirm Password Is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password Doesnot Match Password !!")]
        public string ConfirmPassword { get; set; }
    }
}

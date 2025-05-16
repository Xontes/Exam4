using System.ComponentModel.DataAnnotations;

namespace Exam4.ViewModels
{
    public class RegisterVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

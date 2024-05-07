using System.ComponentModel.DataAnnotations;

namespace MVC.ModelView
{
    public class RegisterMV
    {
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword {  get; set; } = string.Empty;
        public int Department { get; set; }
    }
}

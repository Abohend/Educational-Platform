using System.ComponentModel.DataAnnotations;

namespace MVC.ModelView
{
    public class LoginMV
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}

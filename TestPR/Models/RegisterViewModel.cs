using System.ComponentModel.DataAnnotations;

namespace TestPR.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле обязательное для заполнения")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

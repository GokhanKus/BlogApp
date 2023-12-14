using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
	public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta adresi")]
        public string? Email { get; set; }

        [Required]
        [StringLength(25,ErrorMessage ="{0} alani en az {2} olmalidir", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
	public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta adresi")]
        public string? Email { get; set; }

        [Required]
		[Display(Name = "User Name")]
		public string? UserName { get; set; }

		[Required]
		[Display(Name = "Ad Soyad")]
		public string? Name { get; set; }

        [Required]
        [StringLength(25,ErrorMessage ="{0} alani en az {2} olmalidir", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Parola")]
        [Compare(nameof(Password),ErrorMessage = "Parola eslesmiyor")]
		public string? ConfirmPassword { get; set; }
	}
}

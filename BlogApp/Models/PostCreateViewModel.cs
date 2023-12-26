using DATA.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
	public class PostCreateViewModel
	{
        public int PostId { get; set; }

        [Required]
		[Display(Name = "Baslik")]
		public string? Title { get; set; }

		[Required]
		[Display(Name = "Icerik")]
		public string? Content { get; set; }
		[Required]
		[Display(Name = "Aciklama")]
		public string? Description { get; set; }

		[Required]
		[Display(Name = "Url")]
		public string? Url { get; set; }
        public bool IsActive { get; set; }
		public List<Tag> Tags { get; set; } = new();
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Enums
{
	public enum TagColors
	{
		[Description("Renk")]
		[Display(Name = "mavi")]
		primary,

		[Description("Renk")]
		[Display(Name = "kirmizi")]
		danger,

		[Description("Renk")]
		[Display(Name = "sari")]
		warning,

		[Description("Renk")]
		[Display(Name = "yesil")]
		success,

		[Description("Renk")]
		[Display(Name = "gri")]
		secondary,

		[Description("Renk")]
		[Display(Name = "turkuaz")]
		info,

		[Description("Renk")]
		[Display(Name = "siyah")]
		dark
	}
}

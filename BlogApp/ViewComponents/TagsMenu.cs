using BUSINESS.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
	public class TagsMenu:ViewComponent
	{
		private readonly ITagRepository _tagRepository;
        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task <IViewComponentResult> InvokeAsync()
        {
            return View(await _tagRepository.Tags.ToListAsync());
            //return View("Default.cshtml",_tagRepository.Tags.ToList()); shared/components_tagsmenu altındaki default.cshtml
        }
    }
}

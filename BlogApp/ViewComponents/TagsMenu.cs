using BUSINESS.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
	public class TagsMenu:ViewComponent
	{
		private readonly ITagRepository _tagRepository;
        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public IViewComponentResult Invoke()
        {
            return View(_tagRepository.Tags.ToList());
            //return View("Default.cshtml",_tagRepository.Tags.ToList()); shared/components_tagsmenu altındaki default.cshtml
        }
    }
}

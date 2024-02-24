using AutoMapper;
using BlogApp.Models;
using DATA.Entities;

namespace BlogApp.AutoMapper
{
	public class MappingProfile:Profile
	{
        public MappingProfile()
        {
            CreateMap<PostCreateViewModel, Post>().ReverseMap(); //source, destination
        }
    }
}

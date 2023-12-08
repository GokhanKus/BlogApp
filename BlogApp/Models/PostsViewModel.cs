﻿using DATA.Entities;

namespace BlogApp.Models
{
	public class PostsViewModel
	{
        public List<Post> Posts { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }
}

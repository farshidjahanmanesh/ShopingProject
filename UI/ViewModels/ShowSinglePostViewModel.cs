using EntityModels.DTOs.PostDtos;
using EntityModels.Entities.Posts;
using System;
using System.Collections.Generic;

namespace UI.ViewModels
{
    public class ShowSinglePostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string BaseImage { get; set; }
        public string FullText { get; set; }
        public DateTime PublishDate { get; private set; }
        public bool IsActive { get; private set; }
        public string Slug { get; private set; }
        public int AuthorId { get; set; }
        public List<PostImage> Images { get; set; } = new List<PostImage>();
        public List<PostKeyword> Keywords { get; set; } = new List<PostKeyword>();
        public List<PostCommentDto> Comments { get; set; } = new List<PostCommentDto>();
    }

}

using EntityModels.Entities.BasicEntity;
using EntityModels.Entities.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityModels.DTOs.PostDtos
{
    public class PostDto
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Summery { get; set; }
        [Required]
        public string BaseImage { get; set; }
        [Required]
        public string FullText { get; set; }
        public DateTime PublishDate { get;private set; }
        public bool IsActive { get; private set; }
        public string Slug { get; private set; }
        public int AuthorId { get; set; }
        public List<PostImage> Images { get; set; } = new List<PostImage>();
        public List<PostKeyword> Keywords { get; set; } = new List<PostKeyword>();
        public List<PostCommentDto> Comments { get; set; } = new List<PostCommentDto>();

    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Entities.Post
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string FullText { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int AuthorId { get; set; }
        public List<PostImage> Images { get; set; } = new List<PostImage>();
        public List<PostKeyword> Keywords { get; set; } = new List<PostKeyword>();
        public List<PostComment> Comments { get; set; } = new List<PostComment>();
    }
}

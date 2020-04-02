using System;

namespace EntityModels.Entities.Post
{
    public class PostComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsActive { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

using EntityModels.Entities.Posts;
using System;
using System.ComponentModel.DataAnnotations;

namespace EntityModels.DTOs.PostDtos
{
    public class PostCommentDto 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime PublishDate { get; private set; }
        public bool IsActive { get;private set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }

}

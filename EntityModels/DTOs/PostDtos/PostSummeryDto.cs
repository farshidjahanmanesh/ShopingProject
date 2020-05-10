using System;
using System.ComponentModel.DataAnnotations;

namespace EntityModels.DTOs.PostDtos
{
    public class PostSummeryDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Summery { get; set; }
        [Required]
        public string BaseImage { get; set; }
        public DateTime PublishDate { get; private set; }
        public string Slug { get; private set; }
        public int AuthorId { get; set; }
    }

}

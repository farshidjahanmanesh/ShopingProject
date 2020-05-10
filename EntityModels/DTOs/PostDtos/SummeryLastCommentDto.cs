using System;

namespace EntityModels.DTOs.PostDtos
{
    public class SummeryLastCommentDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; private set; }
        public int PostId { get; set; }
    }

}

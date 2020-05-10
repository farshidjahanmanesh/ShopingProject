using System;

namespace UI.ViewModels
{
    public class PostSummeryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string BaseImage { get; set; }
        public DateTime PublishDate { get; private set; }
        public string Slug { get; private set; }
        public int AuthorId { get; set; }
    }

}

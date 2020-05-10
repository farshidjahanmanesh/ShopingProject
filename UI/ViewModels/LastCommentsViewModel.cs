using System;

namespace UI.ViewModels
{
    public class LastCommentsViewModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime PublishDate { get; private set; }
        public int PostId { get; set; }
    }

}

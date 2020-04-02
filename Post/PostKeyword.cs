namespace EntityModels.Entities.Post
{
    public class PostKeyword
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

namespace EntityModels.Entities.Post
{
    public class PostImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Priority { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

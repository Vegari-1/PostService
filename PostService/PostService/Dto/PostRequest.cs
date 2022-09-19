namespace PostService.Dto
{
    public class PostRequest
    {
        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }

        public Guid AuthorId { get; set; }

        public List<string>? Pictures { get; set; }
    }
}

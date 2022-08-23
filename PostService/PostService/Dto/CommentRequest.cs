namespace PostService.Dto
{
    public class CommentRequest
    {
        public String Content { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }
    }
}

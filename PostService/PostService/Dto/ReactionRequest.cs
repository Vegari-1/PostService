namespace PostService.Dto
{
    public class ReactionRequest
    {
        public bool Positive { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }
    }
}

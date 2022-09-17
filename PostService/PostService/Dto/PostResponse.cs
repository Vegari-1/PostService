namespace PostService.Dto
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
        public int? CommentCount { get; set; }
        public ICollection<CommentResponse>? Comments { get; set; }
        public ICollection<string> Pictures { get; set; }

        public PostResponse() { }
    }
}

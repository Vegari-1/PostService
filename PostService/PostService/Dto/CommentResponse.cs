namespace PostService.Dto
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }


        public CommentResponse(
            Guid id,
            string name,
            string surname,
            string username,
            string image,
            string content,
            DateTime timeStamp)
        {
            Id = id;
            Username = username;
            Name = name;
            Surname = surname;
            Avatar = image;
            Content = content;
            Timestamp = timeStamp;
        }
    }
}

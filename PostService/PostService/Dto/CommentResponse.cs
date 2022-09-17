namespace PostService.Dto
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }


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
            Image = image;
            Content = content;
            TimeStamp = timeStamp;
        }
    }
}

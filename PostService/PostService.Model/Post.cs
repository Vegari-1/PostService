
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Model
{
    [Table("Posts", Schema = "post")]
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid AuthorId { get; set; }

        public int? CommentsNumber { get; set; } = 0;

        public int? LikesNumber { get; set; } = 0;

        public int? DislikesNumber { get; set; } = 0;

        public List<Guid>? Likes { get; set; } = new List<Guid>();

        public List<Guid>? Dislikes { get; set; } = new List<Guid>();

        public List<Image> Images { get; set; }
        public List<Comment>? Comments { get; set; }

        public Post(string content, 
                    DateTime timeStamp, 
                    Guid authorId, 
                    int commentsNumber, 
                    int likesNumber, 
                    int dislikesNumber, 
                    List<Guid> likes, 
                    List<Guid> dislikes)
        {
            Content = content;
            TimeStamp = timeStamp;
            AuthorId = authorId;
            CommentsNumber = commentsNumber;
            LikesNumber = LikesNumber;
            DislikesNumber = DislikesNumber;
            Likes = likes;
            Dislikes = dislikes;
        }

        public Post() { }
    }
}

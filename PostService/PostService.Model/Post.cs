using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Model
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid AuthorId { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }


        public Post(string content, DateTime timeStamp, Guid authorId, int likes, int dislikes)
        {
            Content = content;
            TimeStamp = timeStamp;
            AuthorId = authorId;
            Likes = likes;
            Dislikes = dislikes;
        }

        public Post() { }
    }
}

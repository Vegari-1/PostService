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

        public Post(string content, DateTime timeStamp)
        {
            Content = content;
            TimeStamp = timeStamp;
        }
    }
}

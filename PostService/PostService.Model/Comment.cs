using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Model
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid AuthorId { get; set; }

        public Comment(string content, Guid authorId)
        {
            Content = content;
            AuthorId = authorId;
        }
    }
}

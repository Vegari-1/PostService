using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Model
{
    public class Reaction
    {
        public Guid Id { get; set; }
        public bool Positive { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }

        public Reaction(bool positive, Guid authorId, Guid postId)
        {
            Positive = positive;
            AuthorId = authorId;
            PostId = postId;
        }
    }
}

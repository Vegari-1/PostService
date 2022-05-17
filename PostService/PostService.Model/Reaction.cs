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

        public Reaction(bool positive, Guid authorId)
        {
            Positive = positive;
            AuthorId = authorId;
        }
    }
}

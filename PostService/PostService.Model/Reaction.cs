using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PostService.Model
{

    [Table("Reactions", Schema = "post")]
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

        public Reaction()
        {
        }
    }
}

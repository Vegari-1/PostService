
using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Model
{
    [Table("Comments", Schema = "post")]
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string? Avatar { get; set; }

        public Comment()
        {
        }
    }
}

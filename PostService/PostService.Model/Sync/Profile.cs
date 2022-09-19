using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Model.Sync
{
    [Table("Profiles", Schema = "post")]
    public class Profile
    {
        public Guid Id { get; set; }

        public bool? Public { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string? Avatar { get; set; }

        public Profile()
        {
        }
    }
}

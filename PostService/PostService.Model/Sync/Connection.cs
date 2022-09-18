using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Model.Sync
{
    [Table("Connections", Schema = "post")]
    public class Connection
    {
        public Guid Id { get; set; }
        public Guid Profile1 { get; set; }
        public Guid Profile2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PostService.Model
{

    [Table("Images", Schema = "post")]
    public class Image
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public Guid PostId { get; set; }
        public Image(string content)
        {
            Content = content;
        }

        public Image()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Model
{
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

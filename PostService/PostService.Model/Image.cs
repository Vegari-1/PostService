using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Model
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Location { get; set; }

        public Image(string location)
        {
            Location = location;
        }
    }
}

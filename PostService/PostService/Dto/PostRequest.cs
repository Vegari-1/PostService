using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Dto
{
    public class PostRequest
    {
        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }

        public string AuthorId { get; set; }
        public List<string>? Pictures { get; set; }
    }
}

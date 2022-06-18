using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Model
{
    public class Connection
    {
        public Guid Id { get; set; }

        public Guid Profile1 { get; set; }

        public Guid Profile2 { get; set; }
    }
}

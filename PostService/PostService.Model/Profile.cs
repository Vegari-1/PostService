using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Model
{
    public class Profile
    {
        public Guid Id { get; set; }

        public bool Public { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Profile(Guid id, bool @public, string name, string surname)
        {
            Id = id;
            Public = @public;
            Name = name;
            Surname = surname;
        }

        public Profile()
        {
        }
    }
}

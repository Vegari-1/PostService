﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostService.Model
{
    public class Profile
    {
        public Guid Id { get; set; }

        public bool? Public { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public Guid? ImageId { get; set; }

        public Image Image { get; set; }

        public Profile(Guid id, bool @public, string name, string surname, string username)
        {
            Id = id;
            Public = @public;
            Name = name;
            Surname = surname;
            Username = username;
        }

        public Profile()
        {
        }
    }
}

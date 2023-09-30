using System;
using System.Collections.Generic;
using System.Text;

namespace MOVIE.Models
{
    class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Genre(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
        }

        public Genre() { }

        public override string ToString()
        {
           return Name;
        }
    }
}

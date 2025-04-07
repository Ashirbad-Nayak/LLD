using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD
{
    public class City
    {
        private static int _idCounter = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public MovieManager _movieManager { get; set; } = new MovieManager();
        public TheatreManager _theatreManager { get; set; } = new TheatreManager();

        public City(string name)
        {
            Id = ++_idCounter;
            Name = name;
        }

    }
}

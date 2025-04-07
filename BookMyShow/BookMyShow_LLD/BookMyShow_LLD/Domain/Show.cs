using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain
{
    public class Show
    {
        private static int _idCounter = 0;
        public int Id { get; }
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public Screen Screen { get; set; }
        public Show(Movie movie, DateTime startTime, Screen screen)
        {
            Id = ++_idCounter;
            Movie = movie;
            StartTime = startTime;
            Screen = screen;
        }
    }
}

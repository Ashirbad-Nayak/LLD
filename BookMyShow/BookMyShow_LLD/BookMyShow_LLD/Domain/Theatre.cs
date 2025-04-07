using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain
{
    public class Theatre
    {
        private static int _idCounter = 0;
        public int Id { get; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Screen> Screens { get; set; }
        public Dictionary<Movie,List<Show>> ShowsByMovie { get; set; } = new Dictionary<Movie, List<Show>>();
        public Theatre(string name, string location)
        {
            Id = ++_idCounter;
            Name = name;
            Location = location;
            Screens = new List<Screen>();
        }
        public void AddScreens(List<Screen> screens)
        {
            Screens.AddRange(screens);
        }
        public void RemoveScreen(Screen screen)
        {
            Screens.Remove(screen);
        }
        public void AddShow(Movie movie, Show show)
        {
            if (!ShowsByMovie.ContainsKey(movie))
            {
                ShowsByMovie[movie] = new List<Show>();
            }
            ShowsByMovie[movie].Add(show);
            movie.AddShow(this, show); // Add the show to the movie's list as well
        }

        public void RemoveShow(Movie movie, Show show)
        {
            if (ShowsByMovie.ContainsKey(movie))
            {
                ShowsByMovie.Remove(movie);
                movie.RemoveShow(this, show); // Remove the show from the movie's list as well
            }
        }
        public List<Show> GetShowsByMovie(Movie movie)
        {
            if (ShowsByMovie.ContainsKey(movie))
            {
                return ShowsByMovie[movie];
            }
            return new List<Show>();
        }
        public Dictionary<Movie, List<Show>> GetAllShows()
        {
            if (ShowsByMovie.Count == 0)
            {
                Console.WriteLine("No shows available for this theatre.");
            }
            return ShowsByMovie;
        }
        public void DisplayThatreInfo()
        {
            Console.WriteLine($"Theatre ID: {Id}, Name: {Name}, Location: {Location}");
        }


    }
}

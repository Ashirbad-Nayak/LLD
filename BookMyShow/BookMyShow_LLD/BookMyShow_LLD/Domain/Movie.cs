using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain
{
    public class Movie
    {
        private static int _idCounter = 0;
        public int Id { get; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; } // in minutes
        public DateTime ReleaseDate { get; set; }
        public Dictionary<Theatre, List<Show>> ShowsByTheatre { get; set; } = new Dictionary<Theatre, List<Show>>();
        public Movie(string name, string language, string genre, int duration, DateTime releaseDate)
        {
            Id = ++_idCounter;
            Name = name;
            Language = language;
            Genre = genre;
            Duration = duration;
            ReleaseDate = releaseDate;
        }

        public void AddShow(Theatre theatre, Show show)
        {
            if (!ShowsByTheatre.ContainsKey(theatre))
            {
                ShowsByTheatre[theatre] = new List<Show>();
            }
            ShowsByTheatre[theatre].Add(show);
        }
        public void RemoveShow(Theatre theatre, Show show)
        {
            if (ShowsByTheatre.ContainsKey(theatre))
            {
                ShowsByTheatre[theatre].Remove(show);
                if (ShowsByTheatre[theatre].Count == 0)
                {
                    ShowsByTheatre.Remove(theatre);
                }
            }
        }
        public List<Show> GetShowsByTheatre(Theatre theatre)
        {
            if (ShowsByTheatre.ContainsKey(theatre))
            {
                return ShowsByTheatre[theatre];
            }
            return new List<Show>();
        }
        public Dictionary<Theatre,List<Show>> GetAllShows()
        {
            if (ShowsByTheatre.Count == 0)
            {
                Console.WriteLine("No shows available for this movie.");
            }
            return ShowsByTheatre;
        }

        public void DisplayShowsByTheatre()
        {
            foreach (var theatre in ShowsByTheatre)
            {
                Console.WriteLine($"Theatre: {theatre.Key.Name}, Location: {theatre.Key.Location}");
                foreach (var show in theatre.Value)
                {
                    Console.WriteLine($">>>  Show ID: {show.Id}, Start Time: {show.StartTime}, Screen ID: {show.Screen.Id}");
                }
            }
        }
        public void DisplayMovieDetails()
        {
            Console.WriteLine($"Movie ID: {Id}, Name: {Name}, Language: {Language}, Genre: {Genre}, Duration: {Duration} minutes, Release Date: {ReleaseDate.ToShortDateString()}");
        }


    }
}

using BookMyShow_LLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD
{
    public class MovieManager
    {
        private static int _idCounter = 0;
        public int Id { get; }
        public Dictionary<int,Movie> movies { get; set; } = new Dictionary<int, Movie>();
       
        public MovieManager()
        {
            Id = ++_idCounter;
        }
        public void AddMovie(Movie movie)
        {
            movies.Add(movie.Id, movie);
        }
        public void RemoveMovie(Movie movie)
        {
            movies.Remove(movie.Id);
        }
        public Movie GetMovieById(int id)
        {
            if (movies.ContainsKey(id))
            {
                return movies[id];
            }
            return null;
        }
        public List<Movie> GetAllMovies()
        {
            return movies.Values.ToList();
        }


    }
}

using BookMyShow_LLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD
{
    public class TheatreManager
    {
        private static int _idCounter = 0;
        public int Id { get; }
        public Dictionary<int, Theatre> theatres { get; set; } = new Dictionary<int, Theatre>();
        public TheatreManager()
        {
            Id = ++_idCounter;
        }
        public void AddTheatre(Theatre theatre)
        {
            theatres.Add(theatre.Id, theatre);
        }
        public void RemoveTheatre(Theatre theatre)
        {
            theatres.Remove(theatre.Id);
        }
        public Theatre GetTheatreById(int id)
        {
            if (theatres.ContainsKey(id))
            {
                return theatres[id];
            }
            return null;
        }
        public List<Theatre> GetAllTheatres()
        {
            return theatres.Values.ToList();
        }
    }
}

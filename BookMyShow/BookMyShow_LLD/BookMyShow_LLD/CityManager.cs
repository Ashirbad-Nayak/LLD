using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD
{
    public class CityManager
    {
        private static readonly object _lock = new object();
        private static CityManager _instance;
        public Dictionary<int, City> Cities { get; set; } = new Dictionary<int, City>();
        private CityManager()
        {
            // Private constructor to prevent instantiation from outside
        }
        public static CityManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CityManager();
                    }
                }
                return _instance;
            }
        }
        public void AddCity(City city)
        {
            if (!Cities.ContainsKey(city.Id))
            {
                Cities.Add(city.Id, city);
            }
            else
            {
                Console.WriteLine("City already exists.");
            }
        }
        public void RemoveCity(City city)
        {
            if (Cities.ContainsKey(city.Id))
            {
                Cities.Remove(city.Id);
            }
            else
            {
                Console.WriteLine("City not found.");
            }
        }
        public City GetCityById(int id)
        {
            if (Cities.ContainsKey(id))
            {
                return Cities[id];
            }
            return null;
        }
        public List<City> GetAllCities()
        {
            return Cities.Values.ToList();
        }
        public void DisplayCities()
        {
            foreach (var city in Cities.Values)
            {
                Console.WriteLine($"City ID: {city.Id}, Name: {city.Name}");
            }
        }


    }
}

using BookMyShow_LLD.Domain;
using BookMyShow_LLD.Domain.Enum;

namespace BookMyShow_LLD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BookMyShow!");
            //create some space in console
            Console.WriteLine(new string('-', 50));

            //citymanger
            CityManager cityManager = CityManager.Instance;
            //create city
            List<City> cities = CreateCity();
            foreach (var city in cities)
            {
                cityManager.AddCity(city);
            }
            
            //create theatre
            List<Theatre> theatres = CreateTheatres();
            foreach (var city in cityManager.GetAllCities())
            {
                foreach (var theatre in theatres)
                {
                    city._theatreManager.AddTheatre(theatre);
                }
            }

            //create movie
            List<Movie> movies = CreateMovies();
            List<City> allCities = cityManager.GetAllCities();
            foreach (var movie in movies)
            {
                
                foreach (var city in allCities)
                {
                    city._movieManager.AddMovie(movie);
                }

            }
            
            //create show
            foreach (var city in allCities)
            {
                List<Movie> allMoviesbyCity = city._movieManager.GetAllMovies();
                List<Theatre> allTheatres = city._theatreManager.GetAllTheatres();
                foreach (var movie in allMoviesbyCity)
                {
                    foreach (var theatre in allTheatres)
                    {
                        //for all screen
                        foreach(var screen in theatre.Screens)
                        {
                            //create show for eah screen
                            Show newShow = new Show(movie, DateTime.Now, screen);
                            theatre.AddShow(movie, newShow);
                        }
                    }
                }
            }



            //User selects a city
            City citySelected = cityManager.GetCityById(1); 
            List<Movie> allMovies = citySelected._movieManager.GetAllMovies();
                Console.WriteLine("Movies in city: " + citySelected.Name);
                foreach (var movie in allMovies)
                {
                    movie.DisplayMovieDetails();
                }
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Please Select a movie");


            //user select movie
            Movie movieSelected = allMovies[0];
            Console.WriteLine("Selected Movie: " + movieSelected.Name);

            //get All shows by theatre for the selected movie
            movieSelected.DisplayShowsByTheatre();
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("Please select a theatre and show time");

            //simulating clicking on a theatre_show_combination in ui and getting an id
            Dictionary<Theatre, List<Show>> allShows = movieSelected.GetAllShows();
            //user selects theatre and show time
            Theatre theatreSelected = allShows.First().Key;
            Show show = allShows.First().Value.First();

            //simulating seat selections
            List<Seat> selectedSeats = show.Screen.GetAvailableSeats().
                Where(x => x.Row == 1 && x.seatType.SeatCategory == SeatTypeEnum.SILVER).ToList();

            //display selected theatre and show
            theatreSelected.DisplayThatreInfo();
            Console.WriteLine($"Selected Show: {show.Id},Selected Seats: {string.Join(", ", selectedSeats.Select(x => x.Id))}");
            Console.WriteLine(new string('-', 50));

            BookingManager bookingManager = BookingManager.Instance;
           int bookingId = bookingManager.CreateReservation_Booking(show.Id, selectedSeats);
            Console.WriteLine($"Booking ID: {bookingId}");

            //user selects payment type
            bookingManager.FillPaymentDetailsAndMakePayment(bookingId, "UPI");

            //another user wont see same seat while they are blocked
            List<Seat> availableSeats = show.Screen.GetAvailableSeats();
            Console.WriteLine($"User 2 >>> Available Seats: {string.Join(", ", availableSeats.Select(x => x.Id))}");

            Task.Delay(7000).Wait(); // wait till payment process is completed(success or failure)
            availableSeats = show.Screen.GetAvailableSeats();
            Console.WriteLine($"User 2 >>> Available Seats: {string.Join(", ", availableSeats.Select(x => x.Id))}");




            //Selecting seats again,creating boking again  & Retrying payment
            Console.WriteLine(new string('-', 50));

            Console.WriteLine("User 1 >>> Retrying payment");
            bookingId = bookingManager.CreateReservation_Booking(show.Id, selectedSeats);
            Console.WriteLine($"Booking ID: {bookingId}");
            bookingManager.FillPaymentDetailsAndMakePayment(bookingId, "CreditCard");

            Task.Delay(7000).Wait(); // wait till payment process is completed(success or failure)

            //user 2 : sees only seats that are not booked
            availableSeats = show.Screen.GetAvailableSeats();
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"User 2 >>> Available Seats: {string.Join(", ", availableSeats.Select(x => x.Id))}");

        }

        public static List<City> CreateCity()
        {
            List<City> cities = new List<City>();
            for(int i = 1; i<2;i++){
                City city = new City("Test City "+i);
                cities.Add(city);
            }
            return cities;
        }

        public static List<Movie> CreateMovies()
        {
            List<Movie> movies = new();
            for (int i = 1; i < 2; i++)
            {
                Movie movie = new Movie("Test Movie " + i, "English", "Action", 120, DateTime.Now);
                movies.Add(movie);
            }
            return movies;
        }

        public static List<Theatre> CreateTheatres()
        {
            List<Theatre> theatres = new();
            for (int i = 1; i <= 2; i++)
            {
                Theatre theatre = new Theatre("Test Theatre " + i, "Test Address " + i);
                List<Screen> screens = new();
                for (int j = 1; j <= 2; j++)
                {
                    Screen screen = new Screen();
                    //create seats
                    List<Seat> seats = new();

                    for (int k = 1; k <= 5; k++)
                    {
                        Seat seat = new Seat();
                        seat.Row = 1;
                        seat.seatType = new SeatType(SeatTypeEnum.SILVER, 100);
                        seats.Add(seat);
                    }
                    for (int k = 1; k <= 5; k++)
                    {
                        Seat seat = new Seat();
                        seat.Row = 2;
                        seat.seatType = new SeatType(SeatTypeEnum.GOLD, 200);
                        seats.Add(seat);
                    }
                    for (int k = 1; k <= 5; k++)
                    {
                        Seat seat = new Seat();
                        seat.Row = 3;
                        seat.seatType = new SeatType(SeatTypeEnum.PLATINUM, 300);
                        seats.Add(seat);
                    }
                    screen.AddSeats(seats);
                    screens.Add(screen);
                }
                theatre.AddScreens(screens);
                theatres.Add(theatre);
            }
            return theatres;
        }
        
    }
}

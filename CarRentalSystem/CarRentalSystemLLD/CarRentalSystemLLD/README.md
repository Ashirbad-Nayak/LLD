# Car Rental System

## Project Overview
The Car Rental System is a software application designed to manage car rentals. It allows users to view available cars, book cars, and manage bookings.

## Features
- View available cars
- Book cars
- Manage bookings
- Calculate rental prices
- Different payment strategies (Cash, UPI)

## Filtering And Getting Avaialble Cars Within a Daterange
- 1. if the dataset is not that huge, everytime useer filters witha start date and end date, get all the bookings and check waht all cars are booked within those dates and no available
	and return all cars except the booked cars

- 2.If data set is huge, 
- User selects location + date range

- Backend hits Elasticsearch with: Location (geo or text) or Date filters (based on availability index) or Other filters (price, amenities)

- Results come back fast (from ES + cache)

- User clicks "Book" → real-time check against SQL + lock if available

- Booking is stored in SQL → future availability gets updated

## Availability Precomputation
- ✅ Start with SQL + indexed queries (what we did earlier)

- 🧠 Add availability precomputation once scale increases

- ⚡ Use Redis or Elasticsearch when speed is critical

- 🔐 Always confirm availability at booking time

- we may want to store availability dates of each car.
    Meaning , Prexompute the data and store in ELastic searh.
- {
  "listing_id": 12345,
  "location": {
    "city": "New York",
    "lat": 40.7128,
    "lon": -74.0060
  },
  "price": 150,
  "available_dates": [
    "2025-04-15",
    "2025-04-16",
    "2025-04-17",
    "2025-04-18"
  ],
  "type": "Luxury"
}
- or
- {
  "available_ranges": [
    { "from": "2025-04-01", "to": "2025-04-20" },
    { "from": "2025-05-01", "to": "2025-05-10" }
  ]
}
- Then  query using Elasticsearch range queries 
- Why Use Elasticsearch for This?
🔍 Fast filtering across millions of listings

🗺️ Built-in support for geo-search (e.g., find listings within 10km)

📅 Supports date filters natively

💥 Powerful aggregations (e.g., min/max price by city)
- Feature	        In ES
- Location search	✅ (geo or text)
- Available dates	✅ (via available_dates or ranges)
- Price filters	    ✅ (range filter)
- Full-text search	✅
- Fast multi-criteria filters	✅


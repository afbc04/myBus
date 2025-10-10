# Summary

- **Title:** Book a Trip
- **Description:** A user can book a trip
- **Pre-condition:** Trip must exist
- **Pos-condition:** Trip has a new passenger

# Flow

### Normal Flow

1. User initiates the booking trip process.
2. User selects the trip
3. System registers the current date has the date this user booked the trip & register the price paid
4. System adds a new passenger to the trip

### Exception (3) [Trip is not scheduled]

4. System informs this trip cannot add any passengers


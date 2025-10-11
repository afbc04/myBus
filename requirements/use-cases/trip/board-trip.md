# Summary

- **Title:** Board a Trip
- **Description:** Driver informs to the system that an user got into the bus
- **Pre-condition:** Trip and Passenger must exist & User must own the trip
- **Pos-condition:** Trip's passenger got confirmed

# Flow

### Normal Flow

1. User initiates the boarding trip process.
2. User selects the trip
3. User selects the passenger
4. System registers the current date has the date this user board the trip
5. System updates passenger's status of the trip

### Alternative Flow (3) [User has not booked trip]

4. System saves this user as a new passenger to this trip
5. _Back to step 4_

### Exception (3) [Trip is not scheduled]

4. System informs this trip cannot update any passengers' status


# Summary

- **Title:** Leave a Trip
- **Description:** A user can leave a trip
- **Pre-condition:** Trip must exist
- **Pos-condition:** Trip's passenger leaves

# Flow

### Normal Flow

1. User initiates the leaving trip process.
2. User selects the trip
3. System removes the user in trip's passengers list 

### Exception (3) [Trip is not scheduled]

4. System informs this trip cannot remove any passengers

### Exception (3) [User is a confirmed passenger & User is passenger]

4. System informs this trip cannot remove any passengers

_Driver can remove any passenger_

# Summary

- **Title:** List Trips
- **Description:** The system gives a list of the trips registered
- **Pre-condition:** -
- **Pos-condition:** Trips are listed

# Flow

### Normal Flow

1. User indicates he wants to list trips
2. System list buses, providing informations such as:
    - ID
    - Status
    - Starting Point
    - Ending Point
    - Travel Time
    - Departure Date
    - Arrival Date
    - Number of passengers

### Alternative Flow (2) [Trips with a certain Starting Point only]

- System list trips which starting point is a certain starting point

### Alternative Flow (2) [Trips with a certain Endpoint Point only]

- System list trips which ending point is a certain ending point

### Alternative Flow (2) [Scheduled Trips only]

- System list scheduled trips, ordered by schedule departure date

### Alternative Flow (2) [Trips of a bus only]

- System list trips which bus ID is a certain bus

### Alternative Flow (2) [Trips of a driver only]

- System list trips which driver ID is a certain driver

### Alternative Flow (2) [Trips of a user only]

- System list trips which user is in trip's passengers

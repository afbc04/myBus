# Summary

- **Title:** Create Trip
- **Description:** A driver can create a new trip
- **Pre-condition:** User must be Driver or Administrator & Bus must exists
- **Pos-condition:** New trip is registered in the system

# Flow

### Normal Flow

1. User initiates the creation trip process.
2. User selects the bus
2. User provides the follow details:
    - Schedule Departure Date
    - Arrival Departure Date
3. System validates the information
4. System fills some missing information, such as:
    - busID: the bus ID selected by user
    - driverID: the user's ID
    - status: `SCHEDULED` 
5. System saves the bus

### Exception (3) [Information is invalid]

4. System informs the trip could not be created because information is invalid

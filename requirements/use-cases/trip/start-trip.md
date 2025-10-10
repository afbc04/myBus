# Summary

- **Title:** Start trip
- **Description:** User can start a trip
- **Pre-condition:** Trip must exists and User must be the owner of the trip
- **Pos-condition:** Trip is started

# Flow

### Normal Flow

1. User initiates the starting trip process.
2. User selects the trip
3. System registers:
    - Status of trip: `ONGOING`
    - Registers the current date as Real Departure Date
4. System saves the new information of trip

### Exception (3) [Current date is before the schedule departure date of trip]

4. System informs this trip could not be started because the current date is before the scheduled departure date



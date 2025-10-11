# Summary

- **Title:** Finish trip
- **Description:** User can finish a trip
- **Pre-condition:** Trip must exists and User must be the owner of the trip
- **Pos-condition:** Trip is finished

# Flow

### Normal Flow

1. User initiates the finish trip process.
2. User selects the trip
3. System registers:
    - Status of trip: `COMPLETED`
    - Registers the current date as Real Arrival Date
4. System saves the new information of trip

### Exception (3) [Current date is before the schedule departure date of trip]

4. System informs this trip could not be finished because the current date is before the scheduled departure date



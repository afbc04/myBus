# Summary

- **Title:** Edit Trip
- **Description:** User can update informations about a trip
- **Pre-condition:** Trip must exists and User must be Administrator or the owner of the trip
- **Pos-condition:** Trip's informations are updated

# Flow

### Normal Flow

1. User initiates the edit trip process.
2. User selects the trip
3. User indicates the new information of the trip, such as:
    - ID of bus
    - Driver ID
    - Scheduled Departure Date
    - Schedule Arrival Date
4. System validates the new informations
5. System saves the changes of the trip

### Exception (4) [Informations are invalid]

5. System informs the information provided is invalid

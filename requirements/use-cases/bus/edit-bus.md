# Summary

- **Title:** Edit Bus
- **Description:** User can update informations about a bus
- **Pre-condition:** Bus must exists and User must be Administrator
- **Pos-condition:** Bus' informations are updated

# Flow

### Normal Flow

1. User initiates the edit bus process.
2. User selects the bus
3. User indicates the new information of the bus, such as:
    - Company
    - Level
    - Starting Point
    - Ending Point
    - Total Seats
    - Is wheelchair Accessible?
    - Travel Time
    - Base Price
    - Is bus public?
4. System validates the new informations
5. System saves the changes of the bus

### Exception (4) [Informations are invalid]

5. System informs the information provided is invalid

# Summary

- **Title:** Create Bus
- **Description:** A user can create a new bus
- **Pre-condition:** User must be Administrator
- **Pos-condition:** New bus is registered in the system

# Flow

### Normal Flow

1. User initiates the creation bus process.
2. User provides the follow details:
    - ID
    - Company (optional)
    - Level
    - Starting Point
    - Ending Point
    - Total Seats
    - Is wheelchair accessible? (optional)
    - Travel Time
    - Base Price
    - Is public? (optional)
3. System validates the information
4. System fills some missing information, such as:
    - busCreation: the current date
    - busCreationUser: the user ID
    - public: false _(in case isnt specified)_
    - isActive: true
5. System saves the bus

### Exception (3) [Information is invalid]

4. System informs the bus could not be created because information is invalid

### Exception (3) [ID is taken]

4. System informs the bus could not be created because there is another bus with the name ID

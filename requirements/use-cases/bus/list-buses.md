# Summary

- **Title:** List Buses
- **Description:** The system gives a list of the buses registered
- **Pre-condition:** -
- **Pos-condition:** Buses are listed

# Flow

### Normal Flow

1. User indicates he wants to list buses
2. System list buses, providing informations such as:
    - ID
    - Company
    - Level
    - Starting Point
    - Ending Point
    - Is active?

### Alternative Flow (2) [Buses with a certain Starting Point only]

- System list buses which starting point is a certain starting point

### Alternative Flow (2) [Buses with a certain Endpoint Point only]

- System list buses which ending point is a certain ending point

### Alternative Flow (2) [New buses order]

- System order the list by the newest buses, using the bus creation date

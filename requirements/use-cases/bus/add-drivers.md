# Summary

- **Title:** Adding Drivers
- **Description:** Administrators can add drivers to a bus
- **Pre-condition:** Bus and Driver must exist & User must be Administrator
- **Pos-condition:** Bus has a new driver

# Flow

### Normal Flow

1. User initiates the adding bus' driver process.
2. User selects the bus and selects the driver
3. User indicates he wants to add this driver
4. System registers the current date has the date this driver was assigned to
5. System saves the new driver

### Exception (3) [Driver is a traveller]

4. System informs this account could not be added because its not a driver account


# Summary

- **Title:** Removing Drivers
- **Description:** Administrators can remove a driver of a bus
- **Pre-condition:** Bus and Driver must exist & User must be Administrator
- **Pos-condition:** Bus' driver is removed

# Flow

### Normal Flow

1. User initiates the removing bus' driver process.
2. User selects the bus and selects the driver
3. User indicates he wants to remove this driver
4. System removes this driver

### Exception (3) [Driver is not assigned to this bus]

3. System informs this account could not be removed because its not a bus' driver


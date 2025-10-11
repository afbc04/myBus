# Summary

- **Title:** Changing Driver's Status
- **Description:** User can change driver's status related to a bus
- **Pre-condition:** Bus and Driver must exist & User must be Administrator or the driver itself
- **Pos-condition:** Driver's status is changed

# Flow

### Normal Flow

1. User initiates the changing driver's status process.
2. User selects the bus and selects the driver
3. User indicates the new driver's status
5. System saves the driver's status

### Exception (3) [Driver is not assigned to this bus]

3. System informs this driver's status could not be updated because its not a driver on this bus


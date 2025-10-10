# Summary

- **Title:** Rating Trip
- **Description:** An user can rating a trip
- **Pre-condition:** Trip must exists & User is trip's passenger
- **Pos-condition:** Trip has a new rating

# Flow

### Normal Flow

1. User indicates the trip
2. User indicates the rating _(or nothing)_
3. System saves the rating of the trip

### Exception (2) [Trip is not completed]

3. System informs the user cannot rating a non completed trip


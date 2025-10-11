# Summary

- **Title:** Change User's Level
- **Description:** An user can change anyone's level
- **Pre-condition:** Account must exists & User must be Administrator
- **Pos-condition:** User has a new level

# Flow

### Normal Flow

1. User initiates the changing user level process.
2. User indicates the new level of an user
3. System register the new level

### Alternative Flow (3) [New level is Administrator]

4. System register the current date as the date this user is admin

### Exception (3) [User is a newer Administrator than this administrator account]

3. System informs this account could not be demoted because its an older administrator


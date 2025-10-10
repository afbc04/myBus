# Summary

- **Title:** Add Locality
- **Description:** Administrators can create localities
- **Pre-condition:** User must be Administrator
- **Pos-condition:** System has a new locality

# Flow

### Normal Flow

1. User initiates the add locality process.
2. User provides:
    - Name of locality
    - Level of locality
3. System validates the information provided
4. System saves the new locality, providing the ID to the user 

### Exception (3) [Information is invalid]

4. System informs to user that information provided is invalid

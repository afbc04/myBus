# Summary

- **Title:** Add Country Code
- **Description:** Administrators can create country codes
- **Pre-condition:** User must be Administrator
- **Pos-condition:** System has a new country code

# Flow

### Normal Flow

1. User initiates the add country code process.
2. User provides:
    - Name of country code
    - Abbreviation of country code
3. System validates the information provided
4. System saves the new country code, providing the ID to the user 

### Exception (3) [Information is invalid]

4. System informs to user that information provided is invalid

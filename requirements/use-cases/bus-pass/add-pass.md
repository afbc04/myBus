# Summary

- **Title:** Add Bus Pass
- **Description:** Administrators can create bus pass
- **Pre-condition:** User must be Administrator
- **Pos-condition:** System has a new bus pass

# Flow

### Normal Flow

1. User initiates the add bus pass process.
2. User provides:
    - ID
    - Discount
    - Locality Level
    - Duration
3. System validates the information provided
4. System saves the new bus pass

### Exception (3) [Information is invalid]

4. System informs to user that information provided is invalid

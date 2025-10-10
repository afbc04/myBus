# Summary

- **Title:** Edit Bus Pass
- **Description:** Administrators can edit existing bus pass
- **Pre-condition:** User must be Administrator & bus pass must exists
- **Pos-condition:** Bus Pass' information is updated

# Flow

### Normal Flow

1. User initiates the edit bus pass process.
2. User selects the bus pass
3. User provides the new information, such as:
    - Discount
    - Locality Level
    - Duration
4. System validates the information provided
5. System updates the bus pass

### Exception (4) [Information is invalid]

5. System informs to user that information provided is invalid

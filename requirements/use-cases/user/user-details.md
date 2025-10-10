# Summary

- **Title:** View User Details
- **Description:** System provides information related to the user
- **Pre-condition:** User selected must exists
- **Pos-condition:** User details' are displayed

# Flow

### Normal Flow

1. User indicates he wants to see details of an user
2. System displays information such as:
    - ID
    - Name
    - Email
    - Age
    - Sex
    - Country Code
    - Account Creation
    - Level
    - Is active?
    - Bus pass
    - Is profile public?
    - Inactive Date

### Alternative Flow (2) [Account is private or inactive]

2. System displays information such as:
    - ID
    - Name
    - Country Code
    - Level
    - Is active?
    - Is profile public?

### Alternative Flow (2) [User is Driver, Administrator or the account's owner]

- System displays more information, such as:
    - Is user disable?
    - Birth Date
    - Discount the user has
    - Total price

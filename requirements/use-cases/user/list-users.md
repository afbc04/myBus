# Summary

- **Title:** List Users
- **Description:** The system gives a list of the users registered
- **Pre-condition:** -
- **Pos-condition:** Users are listed

# Flow

### Normal Flow

1. User indicates he wants to list users
2. System list users, providing informations such as:
    - ID
    - Name
    - Level
    - Country Code
    - Is active?

### Alternative Flow (2) [Drivers only]

- System list users which level is Driver

### Alternative Flow (2) [Administrators only]

- System list users which level is Administrator

### Alternative Flow (2) [New users order]

- System order the list by the newest users, using the account creation date

### Alternative Flow (2) [List birthday users only]

- System list users who birthday is the current day

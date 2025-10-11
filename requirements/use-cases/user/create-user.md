# Summary

- **Title:** Create User
- **Description:** A user can create his account to use the system
- **Pre-condition:** -
- **Pos-condition:** New user is registered in the system

# Flow

### Normal Flow

1. User initiates the register user process.
2. User provides the follow details:
    - ID
    - Password
    - Name (optional)
    - Email (optional)
    - Birth Date (optional)
    - Sex
    - If user wants profile to be public
    - Country Code (optional)
3. System validates the information
4. System fills some missing information, such as:
    - Level: Traveller
    - isActive: true
    - accountCreation: current date
    - busPassID: non existent
5. System saves the user

### Alternative Flow (4) [User is the first user registered]

- His level changes to **Administrator**
- Date of Admin is the current date

### Exception (3) [Information is invalid]

4. System informs the user could not be created because information is invalid

### Exception (3) [ID is taken]

4. System informs the user could not be created because there is another user with the name ID

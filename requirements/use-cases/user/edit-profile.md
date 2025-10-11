# Summary

- **Title:** Edit Profile
- **Description:** User can update informations about an account
- **Pre-condition:** Account must exists and User must be the owner of account or Administrator
- **Pos-condition:** Account's informations are updated

# Flow

### Normal Flow

1. User initiates the edit profile process.
2. User selects the account
3. User indicates the new information of the account, such as:
    - Name
    - Email
    - Birth Date
    - Sex
    - Country Code
    - Visibility
    - Password
4. System validates the new informations
5. System saves the changes of the account

### Exception (4) [Informations are invalid]

5. System informs the information provided is invalid

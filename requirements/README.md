# Functionalities - Main Pages

### Users

- Be able to list users, seeing:
    - ID
    - Name
    - Level
    - Is active?
- See who are the drivers and who are the admins
- See new users
- Be able to create a new user
- Be able to update and delete an user
- Admins could promote, demote or reset password of users
- Be able to see information of an user, such as:
    - ID
    - Name
    - Email
    - Birth date & age (informs if its birthday)
    - Sex
    - Country Code
    - Account creation
    - Level
    - Is active?
    - Bus Pass
    - Discounts user has
    - List of travels done, indicating if the user drove or not and rating
    - List of places this user visited
    - List of bus the user drives (DRIVER ONLY)
    - Total price
- Be able to rate a travel
- List of users who makes birthday
- Be able to create, list, change or delete bus pass

### Bus

- Be able to list buses, seeing:
    - ID
    - Company
    - Level
    - Departure point
    - Arrival point
    - Is active?
- See new buses
- Be able to create a bus
- Be able to update and delete a bus
- Be able to see information of a bus, such as:
    - ID
    - Company
    - Level
    - Departure point
    - Arrival point
    - Is active?
    - When was created
    - Total seats
    - Is accessible to disable person?
    - List of travels done
    - List of drivers
    - Time expected
    - Price of travel - Base
- See top N bus with most passengers in a year
- See top N bus with the least median of delay
- See top N bus with the most revenue in a period of time
- Being able to add or remove a driver
- Places could be created or deleted
- List buses that starts or ends in a place

### Trips

- Be able to list travels, seeing:
    - ID
    - Status
    - Departure point
    - Arrival point
    - Time expected
    - Departure date
    - Arrival date
    - Number os passengers
- See travels user can join in
- Be able to create a travel
- Be able to update and delete a travel
- Be able to join or leave a travel
- Driver can add a delay
- Users can rate their travels
- Be able to see information of a travel, such as:
    - ID
    - Status
    - Bus
    - Driver
    - Driver 2
    - Departure point
    - Arrival point
    - Time expected
    - Schedule and Real Departure date
    - Schedule and Real Arrival date
    - Delay
    - List of passengers (showing rating & price)
    - Rating
    - Total price
    - Total seats & number of passengers
- Trips can be order by:
    - Departure point
    - Time expected
    - Rating
- Trip can be filter by:
    - Bus
    - Places (departure or arrival)
    - Driver
    - Status
- List of places

### Statistics

- See some statistics, such as:
    - Number of users
    - Number of users active
    - Number of buses
    - Number of active buses
    - Number of travels
    - Number of travels realized
    - Number of travels cancelled
    - Number of bus pass types
    - Number of places
- Indicating a date, displays information about its subdates, such as:
    - Number of users created
    - Number of travels
    - Number of travels cancelled
    - Percentage of travels with delay
    - Number of passengers
    - Number of unique passengers

# Dataset

---

### Users

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **id** | Unique user identifier | `afbc` |
| **name** | Name of the user | `"André Silva"` |
| **level** | User type | `Traveller` \| `Driver` \| `Administrator` |
| **adminSinceDate** | When this account was administrator? | `"2024-07-32"` |
| **inactiveDate** | When the account was inactive? _(if null, user is active)_ | `2024-11-30 17:37:13` |
| **inactivationAccountUser** | Who inactivated the account? | `fge` |
| **email** | Email address | `"andre@email.com"` |
| **birthDate** | Date of birth | `"1995-04-12"` |
| **sex** | Gender of user | `Male`, `Female`, `Non specified` |
| **countryCode** | Country Code | `"PT"`, `"ES"` |
| **accountCreation** | Date the account was created | `"2023-06-10 16:36:15"` |
| **public** | Is the profile public? | `true`, `false` |
| **isDisable** | Is the person disable? | `true`, `false` |
| **busPassID** | What bus pass this user has _(can be null)_ | `RGL` |
| **busPassValidFrom** | When bus pass was obtained _(can be null)_ | `2024-10-09` |
| **busPassValidTo** | Last day the bus pass is valid _(can be null)_ | `2024-11-09` |

---

### Country Codes

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **id** | Unique country code identifier | `1` |
| **abbreviation** | Abbreviation of the country | `PT` |
| **name** | Name of Country | `Portugal` |

---

### Bus Pass

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **id** | ID of the Bus Pass | `RGL` |
| **discount** | Percentage of discount this bus pass provides | `40.0` |
| **localityLevel** | What level this bus pass includes | `1` \| `2` \| `3` |
| **duration** | How many days this bus pass is valid | `30` |
| **isActive** | Is this bus pass active? | `true`, `false` |

---

### Bus

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **id** | Unique bus identifier | `TGFDA` |
| **company** | Company that owns the bus | `"TUF"`, `"myBus Express"` |
| **level** | Service level of the bus | `Standard`, `Express`, `Luxury` |
| **startingPoint** | Starting location | `"Braga"` |
| **endingPoint** | Ending location | `"Porto"` |
| **busInactiveDate** | When bus stopped being operational? _(if is active, there is no date)_ | `2024-12-06 18:00:45` |
| **inactivationBusUser** | Who inactivated the bus? | `fge` |
| **busCreation** | Date the bus was registered in the system | `"2024-01-15 08:35:23"` |
| **busCreationUser** | Who created the bus? | `afbc` |
| **totalSeats** | Total number of seats | `45` |
| **wheelchairAccessible** | Is bus accessible for wheelchairs? | `true`, `false` |
| **travelTime** | Estimated travel duration (in minutes) | `75` _(1h15min)_ |
| **basePrice** | Base ticket price (in euros) | `2.50` |
| **public** | Is bus available to anyone? | `true`, `false` |

---

### Localities

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **id** | ID of the locality | `1` |
| **name** | Name of locality | `"Famalicão"` |
| **level** | Level of the locality| `1` \| `2` \| `3` |

---

### Drivers

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **busID** | ID of the bus assigned to the driver | `TGFDA` |
| **userID** | ID of the user who is the driver | `afbc` |
| **assignmentDate** | Date the driver was assigned to the bus | `"2024-03-10"` |
| **isActive** | Is the driver active? | `true`, `false` |

---

### Trips

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **id** | Unique trip identifier | `3001` |
| **busID** | ID of the bus used | `TGFDA` |
| **driverID** | ID of the driver assigned | `afbc` |
| **scheduleDepartureDate** | Scheduled date of departure | `"2024-10-05 08:30:00"` |
| **scheduleArrivalDate** | Scheduled date of arrival | `"2024-10-05 09:45:00"` |
| **realDepartureDate** | Real date of departure | `"2024-10-05 08:35:00"` |
| **realArrivalDate** | Real date of arrival | `"2024-10-05 10:05:00"` |
| **status** | Trip state | `SCHEDULED` \| `ONGOING` \| `COMPLETED` \| `CANCELLED` |

### Passengers

| Name of Attribute | Description | Examples |
|-------------------|-------------|-----------|
| **tripID** | ID of the trip | `3001` |
| **userID** | ID of the user in that trip | `afbc` |
| **bookedDate** | Date the user booked the trip | `"2024-10-05 07:35:00"` |
| **boardingDate** | Date the user got on the bus | `"2024-10-05 08:25:00 "` |
| **price** | What was the price paid for the trip (euros) | `1.55` |
| **rating** | User's rating of the trip | `Not Rated` \| `1` \| `2` \| `3` \| `4` \| `5` |

# Use Cases

### User

| File | Description | API Endpoint |
|------|--------------|---------------|
| [change-level.md](use-cases/user/change-level.md) | Changes a user's access level (Traveller, Driver, Admin). | `PATCH /users/{id}` |
| [create-user.md](use-cases/user/create-user.md) | Creates a new user account. | `POST /users` |
| [disable-profile.md](use-cases/user/disable-profile.md) | Deactivates a user's profile. | `DELETE /users/{id}` |
| [edit-profile.md](use-cases/user/edit-profile.md) | Updates a user's profile information. | `PUT /users/{id}` |
| [list-users.md](use-cases/user/list-users.md) | Returns a list of all users. | `GET /users` |
| [user-details.md](use-cases/user/user-details.md) | Get details of an user. | `GET /users/{id}` |

---

### Bus

| File | Description | API Endpoint |
|------|--------------|---------------|
| [add-drivers.md](use-cases/bus/add-drivers.md) | Assigns drivers to a bus. | `POST /drivers/{id}` |
| [bus-details.md](use-cases/bus/bus-details.md) | Get details of a bus. | `GET /buses/{id}` |
| [create-bus.md](use-cases/bus/create-bus.md) | Creates a new bus. | `POST /buses` |
| [disable-bus.md](use-cases/bus/disable-bus.md) | Makes a bus inoperable | `DELETE /buses/{id}` |
| [edit-bus.md](use-cases/bus/edit-bus.md) | Updates information about an existing bus. | `PUT /buses/{id}` |
| [inactive-drivers.md](use-cases/bus/inactive-drivers.md) | Update driver's status of a bus | `PATCH /drivers/{busID}/{driverID}` |
| [list-buses.md](use-cases/bus/list-buses.md) | Returns a list of all buses. | `GET /buses` |
| [list-drivers.md](use-cases/bus/list-drivers.md) | Lists all drivers of a bus. | `GET /drivers/{busID}` |
| [remove-drivers.md](use-cases/bus/remove-drivers.md) | Removes a driver from a bus. | `DELETE /drivers/{busID}/{driverID}` |
| [revenue-bus.md](use-cases/bus/revenue-bus.md) | Calculates revenue of a bus. | `GET /busRevenue/{id}` |
| [top-N-buses-delay.md](use-cases/bus/top-N-buses-delay.md) | Top N buses with the lowest median delay. | `GET /topBuses/medianDelay` |
| [top-N-buses-passengers.md](use-cases/bus/top-N-buses-passengers.md) | Top N buses with the most passengers in a year. | `GET /topBuses/passengersYear/{year}` |
| [top-N-buses-revenue.md](use-cases/bus/top-N-buses-revenue.md) | Top N buses with the most revenue. | `POST /topBuses/revenue` |

---

### Trip

| File | Description | API Endpoint |
|------|--------------|---------------|
| [board-trip.md](use-cases/trip/board-trip.md) | Driver confirms passenger of a trip | `PATCH /passengers/{tripID}/{passengerID}` |
| [book-trip.md](use-cases/trip/book-trip.md) | User books a trip. | `POST /passengers/{tripID}` |
| [cancel-trip.md](use-cases/trip/cancel-trip.md) | Cancels a trip. | `DELETE /trips/{id}` |
| [create-trip.md](use-cases/trip/create-trip.md) | Creates a new trip. | `POST /trips` |
| [edit-trip.md](use-cases/trip/edit-trip.md) | Updates trip. | `PUT /trips/{id}` |
| [finish-trip.md](use-cases/trip/finish-trip.md) | Marks a trip as completed. | `PATCH /trips/{id}` |
| [leave-trip.md](use-cases/trip/leave-trip.md) | Passenger leaves from a trip. | `DELETE /passengers/{tripID}/{passengerID}` |
| [list-passengers.md](use-cases/trip/list-passengers.md) | Lists all passengers of a trip. | `GET /passengers/{id}` |
| [list-trips.md](use-cases/trip/list-trips.md) | Lists all trips. | `GET /trips` |
| [rating-trip.md](use-cases/trip/rating-trip.md) | Passenger rates a completed trip. | `PATCH /trips/{id}` |
| [start-trip.md](use-cases/trip/start-trip.md) | Marks the start of a trip. | `PATCH /trips/{id}` |
| [trip-details.md](use-cases/trip/trip-details.md) | Get details of a trip. | `GET /trips/{id}` |

---

### Bus Pass

| File | Description | API Endpoint |
|------|--------------|---------------|
| [add-pass.md](use-cases/bus-pass/add-pass.md) | Creates a new bus pass. | `POST /busPasses` |
| [delete-pass.md](use-cases/bus-pass/delete-pass.md) | Deletes a bus pass. | `DELETE /busPasses/{id}` |
| [edit-pass.md](use-cases/bus-pass/edit-pass.md) | Updates an existing bus pass. | `PUT /busPasses/{id}` |
| [list-pass.md](use-cases/bus-pass/list-pass.md) | Lists all bus passes. | `GET /busPasses` |
| [pass-details.md](use-cases/bus-pass/pass-details.md) | Get details of a specific bus pass. | `GET /busPasses/{id}` |
| [replace-pass-user.md](use-cases/bus-pass/replace-pass-user.md) | Replaces a bus pass of an user. | `PATCH /users/{id}` |

---

### Country Code

| File | Description | API Endpoint |
|------|--------------|---------------|
| [add-country.md](use-cases/country-code/add-country.md) | Adds a new country code. | `POST /countryCodes` |
| [delete-country.md](use-cases/country-code/delete-country.md) | Deletes a country code. | `DELETE /countryCodes/{id}` |
| [edit-country.md](use-cases/country-code/edit-country.md) | Updates a country code. | `PUT /countryCodes/{id}` |
| [list-countries.md](use-cases/country-code/list-countries.md) | Lists all countries codes. | `GET /countryCodes` |

---

### Locality

| File | Description | API Endpoint |
|------|--------------|---------------|
| [add-locality.md](use-cases/locality/add-locality.md) | Adds a new locality. | `POST /localities` |
| [delete-locality.md](use-cases/locality/delete-locality.md) | Deletes a locality. | `DELETE /localities/{id}` |
| [edit-locality.md](use-cases/locality/edit-locality.md) | Updates a locality. | `PUT /localities/{id}` |
| [list-localities.md](use-cases/locality/list-localities.md) | Returns all localities. | `GET /localities` |
| [locality-of-users.md](use-cases/locality/locality-of-users.md) | Lists all localities visited by an user. | `GET /localitiesUser/{id}` |

---

### Statistic

| File | Description | API Endpoint |
|------|--------------|---------------|
| [date-statistics.md](use-cases/statistic/date-statistics.md) | Get system statistics of a date. | `GET /statistics/dates` |
| [general-infos.md](use-cases/statistic/general-infos.md) | Get general system informations. | `GET /statistics/general` |

---

# Validations

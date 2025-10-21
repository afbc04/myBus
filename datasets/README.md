# Description

This directory has some datasets related to this server:
- **[Clean Data](clean/)** : this directory has correct datasets to be imported directly into database
- **[Testing Data](tests/)** : this directory has datasets to test the validation mechanisms of WebAPI

# Importing datasets

**NOTE** : You should be in `datasets/` directory & SQL Tables must have been created _(API creates the tables in case they dont exist)_

## Import

    chmod +x import_datasets.sh
    ./import_datasets.sh
    docker exec -it mybus-database psql -U [database user] -d myBus

## Copying

##### Country Codes

    DELETE FROM CountryCodes;
    \copy CountryCodes(id, name) FROM '/tmp/country_codes.csv' DELIMITER ',' CSV HEADER;

##### Bus Passes

    DELETE FROM BusPasses;
    \copy BusPasses(id, discount, localityLevel, duration, active) FROM '/tmp/bus_passes.csv' DELIMITER ',' CSV HEADER;

##### Users

    DELETE FROM Users;
    \copy Users(id,name,level,adminSinceDate,inactiveDate,inactiveAccountUser,email,birthDate,sex,countryCode,accountCreation,public,disablePerson,busPassID,busPassValidFrom,busPassValidUntil,salt,password) FROM '/tmp/users.csv' DELIMITER ',' CSV HEADER;


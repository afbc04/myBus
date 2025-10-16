# Description

This directory has some datasets related to this server:
- **[Clean Data](clean/)** : this directory has correct datasets to be imported directly into database
- **[Testing Data](tests/)** : this directory has datasets to test the validation mechanisms of WebAPI

# Importing datasets

**NOTE** : You should be in `datasets/` directory.

## Import

    chmod +x import_datasets.sh
    ./import_datasets.sh
    docker exec -it mybus-database psql -U [database user] -d myBus

## Copying

##### Country Codes

    DELETE FROM CountryCodes;
    \copy CountryCodes(id, name) FROM '/tmp/country_codes.csv' DELIMITER ',' CSV HEADER;

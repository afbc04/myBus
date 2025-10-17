DELETE TABLE Users;
DELETE TABLE BusPasses;
DELETE TABLE CountryCodes;

CREATE TABLE IF NOT EXISTS CountryCodes (
  id CHAR(3) PRIMARY KEY,
  name VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS BusPasses (
  id CHAR(3) PRIMARY KEY,
  discount NUMERIC(5,2) NOT NULL,
  localityLevel SMALLINT NOT NULL,
  duration INT NOT NULL,
  isActive BOOLEAN NOT NULL
);

CREATE TABLE IF NOT EXISTS Users (

  id VARCHAR(15) PRIMARY KEY,
  name VARCHAR(50) NULL,
  level CHAR(1) NOT NULL,
  adminSinceDate DATE NULL,
  inactiveDate DATE NULL,
  inactiveAccountUser VARCHAR(15) NULL,
  email VARCHAR(30) NULL,
  birthDate DATE NULL,
  sex CHAR(1) NOT NULL,
  countryCode CHAR(3) NULL,
  accountCreation TIMESTAMP NOT NULL,
  isPublic BOOLEAN NOT NULL,
  isDisablePerson BOOLEAN NULL,
  busPassID INT NULL,
  busPassValidFrom DATE NULL,
  busPassValidUntil DATE NULL,

  CONSTRAINT fk_inactiveAccountUser FOREIGN KEY (inactiveAccountUser)
    REFERENCES Users(id)
    ON DELETE SET NULL,
  CONSTRAINT fk_countryCode FOREIGN KEY (countryCode)
    REFERENCES CountryCodes(id)
    ON DELETE SET NULL

);

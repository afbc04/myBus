-- Country Codes
SELECT * FROM CountryCodes;
SELECT * FROM CountryCodes ORDER BY name ASC LIMIT 3 OFFSET 0;
SELECT * FROM CountryCodes WHERE id = 'PRT';
SELECT COUNT(*) FROM CountryCodes;

DELETE FROM CountryCodes;
DELETE FROM CountryCodes WHERE id = 'PRT';

INSERT INTO CountryCodes (id,name) VALUES ('PRT','Portugal');

UPDATE CountryCodes SET name = 'Spain' WHERE id = 'PRT';
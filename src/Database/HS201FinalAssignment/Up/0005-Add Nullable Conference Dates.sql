ALTER TABLE Conference DROP COLUMN StartDate
ALTER TABLE Conference DROP COLUMN EndDate

ALTER TABLE Conference ADD StartDate DATETIME NULL
ALTER TABLE Conference ADD EndDate DATETIME NULL
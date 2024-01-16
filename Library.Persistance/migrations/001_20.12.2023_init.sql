CREATE TABLE IF NOT EXISTS "Books"
(
    Id      SERIAL PRIMARY KEY,
    ISBN    INT NOT NULL,
    Title   VARCHAR NOT NULL,
    Author  VARCHAR NOT NULL
);

CREATE TYPE Gender AS ENUM ('male', 'female');

CREATE TABLE IF NOT EXISTS "Users"
(
    Id          SERIAL PRIMARY KEY,
    FirstName   VARCHAR NOT NULL,
    LastName    VARCHAR NOT NULL,
    Patronymic  VARCHAR,
    BirthDay    TIMESTAMP WITH TIME ZONE,
    Address     VARCHAR NOT NULL,
    Gender Gender NOT NULL
);
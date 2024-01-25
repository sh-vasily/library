CREATE TABLE IF NOT EXISTS books
(
    id      INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    isbn    INT NOT NULL,
    title   VARCHAR NOT NULL,
    author  VARCHAR NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_books_title
ON books(title);

CREATE INDEX IF NOT EXISTS idx_books_author
ON books(author);

CREATE TYPE Gender AS ENUM ('male', 'female');

CREATE TABLE IF NOT EXISTS users
(
    id           INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    first_name   VARCHAR NOT NULL,
    last_name    VARCHAR NOT NULL,
    patronymic   VARCHAR,
    birthday     TIMESTAMP WITH TIME ZONE,
    address      VARCHAR NOT NULL,
    gender       Gender NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_users_firstname
ON users(first_name);

CREATE INDEX IF NOT EXISTS idx_users_lastname
ON users(last_name);

CREATE TABLE IF NOT EXISTS books_instances
(
    id           INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    book_id      INTEGER,    CONSTRAINT fk_book
        FOREIGN KEY(book_id)
            REFERENCES books(id)
);

CREATE TABLE IF NOT EXISTS borrowed_books
(
    id           INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    book_id      INTEGER,
    user_id      INTEGER,
    borrow_date  TIMESTAMP WITH TIME ZONE NOT NULL,
    return_date  TIMESTAMP WITH TIME ZONE NULL DEFAULT NULL,
    CONSTRAINT fk_books_instances
        FOREIGN KEY(book_id)
            REFERENCES books_instances(id),
    CONSTRAINT fk_user
        FOREIGN KEY(user_id)
            REFERENCES users(id)
);
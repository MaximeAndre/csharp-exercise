CREATE USER postgres SUPERUSER;
create table IF NOT EXISTS user_info(
    id SERIAL PRIMARY KEY,
    login varchar(50) UNIQUE NOT NULL,
    password varchar(100) NOT NULL,
    first_name varchar(50) NOT NULL,
    last_name varchar(50) NOT NULL,
    email varchar(320) NOT NULL
)
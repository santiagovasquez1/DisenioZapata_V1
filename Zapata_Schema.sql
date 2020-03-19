CREATE TABLE clients (
	client_id INTEGER UNSIGNED PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL UNIQUE,
	birthdate DATETIME,
	gender ENUM('M','F','ND') NOT NULL,
	active TINYINT(1) NOT NULL DEFAULT 1,
	created_ad TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	update_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE users (
	`user_id` INTEGER UNSIGNED PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(50) NOT NULL,
	`password` VARCHAR(50) NOT NULL,
	email VARCHAR(100) NOT NULL UNIQUE,
	industry VARCHAR(50),
	country VARCHAR(3),
	active TINYINT(1) NOT NULL DEFAULT 1,
	created_ad TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	update_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS operations(
	operation_id INTEGER UNSIGNED PRIMARY KEY AUTO_INCREMENT,
	`user_id` INTEGER UNSIGNED,
	ip_dress INTEGER UNSIGNED,
	created_ad TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	update_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	finished TINYINT(1) NOT NULL
);

CREATE USER 'santiagovasquez'@'localhost' IDENTIFIED BY '12345';

GRANT INSERT
ON Zapatas. * 
TO 'santiagovasquez'@'localhost';

GRANT UPDATE
ON Zapatas. * 
TO 'santiagovasquez'@'localhost';
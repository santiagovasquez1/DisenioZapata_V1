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
	`password` VARCHAR(130) NOT NULL,
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

--Querys

INSERT INTO users(name,`password`,email,industry,country)
VALUES();

select email from users
WHERE email LIKE ''

GRANT SELECT ON Zapatas.users TO 'santiagovasquez'@'localhost';

CREATE USER 'clientes'@'zapatas.cidxy8evidix.us-east-1.rds.amazonaws.com' IDENTIFIED BY 'cliente123';
GRANT INSERT ON zapatas.users TO 'clientes'@'zapatas.cidxy8evidix.us-east-1.rds.amazonaws.com';
GRANT SELECT ON zapatas.users TO 'clientes'@'zapatas.cidxy8evidix.us-east-1.rds.amazonaws.com';

select user, password from mysql.user;


UPDATE USER
SET PASSWORD = PASSWORD('Atila_1205')
WHERE USER = 'clientes' 
AND HOST = 'zapatas.cidxy8evidix.us-east-1.rds.amazonaws.com';

set password for clientes @'zapatas.cidxy8evidix.us-east-1.rds.amazonaws.com' = PASSWORD('cl13nt3s123');

update user set password=PASSWORD('qwertyqwerty') where user='clientes';

AUTHENTICATION_STRING

CREATE USER 'clientes'@'localhost' IDENTIFIED BY 'cliente123';
GRANT INSERT ON zapatas.users TO 'clientes'@'localhost';
GRANT SELECT ON zapatas.users TO 'clientes'@'localhost';
GRANT INSERT ON zapatas.operations TO 'clientes'@'localhost';
GRANT SELECT ON zapatas.operations TO 'clientes'@'localhost';

INSERT INTO operations(`user_id`, ip_dress)
VALUES(4,605296783);

mysql -h zapatas.cidxy8evidix.us-east-1.rds.amazonaws.com -P 3306 -u svasquez -p

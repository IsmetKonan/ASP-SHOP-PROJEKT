Dieses Projekt wurde von I.K erstellt
Bevor dieses Projekt gestartet wird, 
muss eine lokale MySQL-Datenbank von ihnen selbst erstellt werden:


Datenbank name: shopdb

 - BEFEHL: 
CREATE DATABASE shopdb;

in shopdb

- BEFEHL: 
USE shopdb;

Dann diesen Code ausführen,
um die nötigen Komponenten in der Lokalen Datenbank zu erstellen:

- BEFEHLE:

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) UNIQUE,
    PasswordHash VARCHAR(255),
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Address VARCHAR(255),
    Email VARCHAR(100),
    Phone VARCHAR(30),
    IsAdmin BOOLEAN DEFAULT 0
);

CREATE TABLE Articles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Description VARCHAR(255),
    Quantity INT,
    Price DECIMAL(10,2)
);


- In Appsettings.json
das Passwort auf ihr Lokales MYSQL Passwort setzen.

- alls letztes sich selbst durch commandprompt oder editor bei admin hinzufügen

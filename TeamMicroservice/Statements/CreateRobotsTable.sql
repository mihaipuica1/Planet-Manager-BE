CREATE TABLE `robots` (
`Id` INT NOT NULL PRIMARY KEY,
`Name` VARCHAR(45) NOT NULL,
`TeamId` INT,
FOREIGN KEY (TeamId) REFERENCES teams(Id) 
);
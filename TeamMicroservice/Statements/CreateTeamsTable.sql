CREATE TABLE `teams` (
`Id` INT NOT NULL PRIMARY KEY,
`Name` VARCHAR(45) NOT NULL,
`CaptainId` INT NOT NULL,
FOREIGN KEY (CaptainId) REFERENCES captains(Id)
);
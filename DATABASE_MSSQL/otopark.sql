create database sensem;
USE sensem;

CREATE TABLE Araba (
    ArabaID INT IDENTITY(1,1) PRIMARY KEY,
    GirisSaati Varchar(20) NOT NULL,
	GirisTarihi Varchar(20) NOT NULL
);
CREATE TABLE ArabaCikis2 (
    ArabaID INT IDENTITY(1,1) PRIMARY KEY,
    GirisTarihi VARCHAR(20) NOT NULL,
    GirisSaati VARCHAR(20) NOT NULL,
    CikisTarihi VARCHAR(20) NOT NULL,
    CikisSaati VARCHAR(20) NOT NULL,
    OdemeTutari DECIMAL(10, 2) NOT NULL
);


INSERT INTO Araba (GirisSaati, GirisTarihi) VALUES ('10:30:00', '22.05.2024');

INSERT INTO ArabaCikis2 (GirisTarihi, GirisSaati, CikisTarihi, CikisSaati, OdemeTutari) 
VALUES ('22.05.2024', '10:30:00', '22.05.2024', '11:30:00', 100.00);

SELECT * FROM ArabaCikis2;
select * from Araba


delete from Araba


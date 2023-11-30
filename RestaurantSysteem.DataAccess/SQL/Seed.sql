CREATE DATABASE RestaurantSysteem;
GO

USE RestaurantSysteem;

CREATE TABLE [dbo].[Tafel]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Naam] VARCHAR(MAX) NULL, 
    [AantalPersonen] INT NULL, 
    [Allergenen] VARCHAR(MAX) NULL, 
    [WinePairing] BIT NULL DEFAULT 0, 
    [Voertaal] VARCHAR(MAX) NULL 
);

CREATE TABLE [dbo].[Gerecht] (
    [GerechtId] INT           IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Naam]      VARCHAR (MAX) NOT NULL
);

CREATE TABLE [dbo].[Menu] (
    [Id]            INT           NOT NULL,
    [Name]          VARCHAR (MAX) NOT NULL,
    [Voorgerecht]   INT           NOT NULL FOREIGN KEY (Voorgerecht) REFERENCES Gerecht(GerechtId),
    [Tussengerecht] INT           NOT NULL FOREIGN KEY (Tussengerecht) REFERENCES Gerecht(GerechtId),
    [Hoofdgerecht]  INT           NOT NULL FOREIGN KEY (Hoofdgerecht) REFERENCES Gerecht(GerechtId),
    [Nagerecht]     INT           NOT NULL FOREIGN KEY (Nagerecht) REFERENCES Gerecht(GerechtId),
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[MenuTafel] (
    [MenuId]  INT NOT NULL,
    [TafelId] INT NOT NULL,
    [Aantal]  INT NOT NULL,
    PRIMARY KEY ([MenuId], [TafelId]),
    FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([Id]),
    FOREIGN KEY ([TafelId]) REFERENCES [dbo].[Tafel] ([Id])
);

INSERT INTO [dbo].[Tafel] (Id, Naam, AantalPersonen, Allergenen, WinePairing, Voertaal) VALUES (1, 'Tafel 1', 0, '', 0, 'Nederlands');
INSERT INTO [dbo].[Tafel] (Id, Naam, AantalPersonen, Allergenen, WinePairing, Voertaal) VALUES (2, 'Tafel 2', 0, '', 0, 'Nederlands');
INSERT INTO [dbo].[Tafel] (Id, Naam, AantalPersonen, Allergenen, WinePairing, Voertaal) VALUES (3, 'Tafel 3', 0, '', 0, 'Nederlands');
INSERT INTO [dbo].[Tafel] (Id, Naam, AantalPersonen, Allergenen, WinePairing, Voertaal) VALUES (4, 'Tafel 4', 0, '', 0, 'Nederlands');
INSERT INTO [dbo].[Tafel] (Id, Naam, AantalPersonen, Allergenen, WinePairing, Voertaal) VALUES (5, 'Tafel 5', 0, '', 0, 'Nederlands');
INSERT INTO [dbo].[Tafel] (Id, Naam, AantalPersonen, Allergenen, WinePairing, Voertaal) VALUES (6, 'Tafel 6', 0, '', 0, 'Nederlands');

DBCC CHECKIDENT ('Gerecht', RESEED, 0)

INSERT INTO Gerecht (Naam) VALUES ('Dorade');
INSERT INTO Gerecht (Naam) VALUES ('Zwezerik');
INSERT INTO Gerecht (Naam) VALUES ('Lam');
INSERT INTO Gerecht (Naam) VALUES ('Rabarber');

INSERT INTO Menu (Id, Name, Voorgerecht, Tussengerecht, Hoofdgerecht, Nagerecht) VALUES (1, 'TravelMenu', 0, 1, 2, 3);

INSERT INTO Gerecht (Naam) VALUES ('Avocado');
INSERT INTO Gerecht (Naam) VALUES ('Lentesalade');
INSERT INTO Gerecht (Naam) VALUES ('Asperges');

INSERT INTO Menu (Id, Name, Voorgerecht, Tussengerecht, Hoofdgerecht, Nagerecht) VALUES (2, 'VegaMenu', 4, 5, 6, 3);
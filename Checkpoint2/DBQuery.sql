 If(db_id(N'WCS_CHECKPOINT2') IS NULL)
 BEGIN
	CREATE DATABASE  WCS_CHECKPOINT2
 END
GO

USE WCS_CHECKPOINT2

DROP TABLE IF EXISTS [AgendaEvent]
DROP TABLE IF EXISTS CampusCursusPerson
DROP TABLE IF EXISTS CampusCursus
DROP TABLE IF EXISTS Cursus
DROP TABLE IF EXISTS Campus
DROP TABLE IF EXISTS PersonPerson
DROP TABLE IF EXISTS Person
DROP TABLE IF EXISTS [Event]
DROP TABLE IF EXISTS Agenda
DROP TABLE IF EXISTS [Function]
DROP PROCEDURE IF EXISTS [sp_getPersonByName]
DROP PROCEDURE IF EXISTS [sp_getAgendaByIdPerson]
DROP PROCEDURE IF EXISTS [sp_getEventsByIdAgenda]
DROP PROCEDURE IF EXISTS [sp_getEventsByPersonName]

CREATE TABLE [Function] (
	[Function_Id] INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(30)
)


CREATE TABLE Agenda (
	[Agenda_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[Name] VARCHAR(50)
)

CREATE TABLE [Event] (
	[Event_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[Name] VARCHAR(50)  NOT NULL,
	[StartDate] DATETIME NOT NULL,
	[EndDate] DATETIME NOT NULL,
	[Description] TEXT NULL
)

CREATE TABLE Person (
	[Person_Id] INT PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(150),
	[FK_Function_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Function_Id]) REFERENCES [Function] ([Function_Id]),
	[FK_Agenda_id] INT NOT NULL,
	FOREIGN KEY ([FK_Agenda_Id]) REFERENCES [Agenda] ([Agenda_Id])
)

CREATE TABLE PersonPerson (
	[PersonPerson_Id] INT PRIMARY KEY IDENTITY(1,1),
	[FK_Person_lead_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Person_Id]) REFERENCES [Person] ([Person_Id]),
	[FK_Person_id] INT NOT NULL,
	FOREIGN KEY ([FK_Person_Id]) REFERENCES [Person] ([Person_Id])
)

CREATE TABLE Campus (
	[Campus_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[Name] VARCHAR(50) NOT NULL,
	[Number]  VARCHAR(50)  NULL,
	[Street] VARCHAR(50)  NULL,
	[Zipcode] VARCHAR(10)  NULL,
	[City] VARCHAR(50)  NULL,
	[FK_Agenda_id] INT NOT NULL,
	FOREIGN KEY ([FK_Agenda_Id]) REFERENCES [Agenda] ([Agenda_Id])
)

CREATE TABLE Cursus (
	[Cursus_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[Name] VARCHAR(50)
)

CREATE TABLE CampusCursus (
	[CampusCursus_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[FK_Cursus_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Cursus_Id]) REFERENCES [Cursus] ([Cursus_Id]),
	[FK_Campus_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Campus_Id]) REFERENCES [Campus] ([Campus_Id]),
)

CREATE TABLE CampusCursusPerson (
	[CampusCursusPerson_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[FK_Person_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Person_Id]) REFERENCES [Person] ([Person_Id]),
	[FK_CampusCursus_Id] INT NOT NULL,
	FOREIGN KEY ([FK_CampusCursus_Id]) REFERENCES [CampusCursus] ([CampusCursus_Id])
)


CREATE TABLE [AgendaEvent] (
	[AgendaEvent_Id] INT PRIMARY KEY IDENTITY(1,1),	
	[FK_Agenda_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Agenda_Id]) REFERENCES [Agenda] ([Agenda_Id]),
	[FK_Event_Id] INT NOT NULL,
	FOREIGN KEY ([FK_Event_Id]) REFERENCES [Event] ([Event_Id])
)


SET NOCOUNT ON

PRINT('Insertion des fonctions START')
	INSERT INTO [Function] ([Name]) VALUES ('Student'), ('Former'), ('LeadFormer')
PRINT('Insertion des fonctions END')
GO
PRINT('Insertion des cursus START')
	INSERT INTO [Cursus] ([Name]) VALUES ('PHP'), ('C#'), ('Java'), ('SQL'), ('Coloriage')
PRINT('Insertion des cursus END')
GO
PRINT('Insertion des Campus START')
	INSERT INTO [Agenda] ([Name]) VALUES ('Agenda Strasbourg'), ('Agenda Paris'), ('Agenda Lyon'), ('Agenda Marseille'), ('Agenda Lille')
	INSERT INTO [Campus] ([Name], FK_Agenda_id) VALUES ('Strasbourg', 1), ('Paris', 2), ('Lyon', 3), ('Marseille', 4), ('Lille', 5)
PRINT('Insertion des Campus END')
GO
PRINT('Insertion des personnes + Agenda (Eleve) START')

DECLARE @cpt AS int = 1

WHILE(@cpt < 201)
BEGIN
	DECLARE @leader INT 
	DECLARE @function INT = 1
	IF @cpt < 2
	BEGIN 
		SET @function = 3
	END
	ELSE
	IF @cpt < 11
	BEGIN
		SET @function = 2
		SELECT top(1) @leader = Person_Id FROM person where FK_Function_Id = 3 ORDER BY NEWID()
	END 
	ELSE
	BEGIN
		SET @function = 1
		SELECT top(1) @leader = Person_Id FROM person where FK_Function_Id = 2 ORDER BY NEWID()
	END
	
	
	INSERT INTO [Agenda] ([Name]) select 'AGENDA ' + CAST(@cpt AS VARCHAR)
	INSERT INTO [Person] ([Name], FK_Function_Id, FK_Agenda_id ) select 'Name' + CAST(@cpt AS VARCHAR),  @function, @@IDENTITY

	IF @cpt >= 2  
	BEGIN
		INSERT INTO PersonPerson ([FK_Person_lead_Id], [FK_Person_Id]) VALUES (@leader, @@IDENTITY)
	END


	SET @cpt = @cpt + 1
END
GO

PRINT('Insertion des events END')
DECLARE @cpt INT = 1

While(@cpt < 100)
BEGIN

	INSERT INTO [Event] ([Name], [StartDate], [EndDate]) VALUES ('Event' + CAST(@cpt AS VARCHAR), DATEADD(day, ABS(CHECKSUM(NewId())) % 50, GETDATE()), DATEADD(day, ABS(CHECKSUM(NewId())) % 50 + 51, GETDATE()))

	SET @cpt = @cpt + 1
END


PRINT('Insertion des events END')

PRINT('Insertion AgendaEvent START')
INSERT INTO AgendaEvent (FK_Agenda_id, FK_Event_Id)
SELECT TOP(1500) Agenda.Agenda_Id,[Event].Event_Id  FROM [Agenda]  CROSS JOIN  [Event]  ORDER BY NEWID()
PRINT('Insertion AgendaEvent END')

PRINT('Insertion CampusCursus START')
INSERT INTO CampusCursus (FK_Campus_Id, FK_Cursus_Id)
SELECT Campus_Id, Cursus_Id FROM Campus CROSS JOIN Cursus
PRINT('Insertion CampusCursus End')

PRINT ('AJout des personnes par campus et cursus START')
INSERT INTO CampusCursusPerson (FK_CampusCursus_Id, FK_Person_Id)
SELECT TOP (1000) [CampusCursus_Id], Person_Id
  FROM CampusCursus
  CROSS JOIN PERSON
  ORDER BY NEWID()
PRINT ('AJout des personnes par campus et cursus END')
GO

CREATE PROCEDURE [sp_getPersonByName]
	@Name VARCHAR(50)
AS
	SELECT        Person.Name AS PersonName, [Function].Name AS FunctionName, Person.Person_Id, Person.FK_Agenda_id
	FROM            Person INNER JOIN
                         [Function] ON Person.FK_Function_Id = [Function].Function_Id WHERE Person.[Name] = @Name
GO

CREATE PROCEDURE [sp_getAgendaByIdPerson]
	@idPerson INT
AS
SELECT        Agenda.Name, Agenda.Agenda_Id
FROM            Person INNER JOIN
                         Agenda ON Person.FK_Agenda_id = Agenda.Agenda_Id
WHERE        (Person.Person_Id = @idPerson)
GO

CREATE PROCEDURE [sp_getEventsByIdAgenda]
	@idAgenda INT
AS
SELECT        Event.Event_Id, Event.Name, Event.StartDate, Event.EndDate, Event.Description
FROM            Agenda INNER JOIN
                         AgendaEvent ON Agenda.Agenda_Id = AgendaEvent.FK_Agenda_Id INNER JOIN
                         Event ON AgendaEvent.FK_Event_Id = Event.Event_Id
WHERE        (Agenda.Agenda_Id = @idAgenda)
ORDER BY Event.StartDate, Event.EndDate
GO

CREATE PROCEDURE [sp_getEventsByPersonName]
	@Name VARCHAR(50)
AS
	SELECT        Person.Name, Event.Name, Event.EndDate, Event.StartDate
	FROM            Person INNER JOIN
							 Agenda ON Person.FK_Agenda_id = Agenda.Agenda_Id INNER JOIN
							 AgendaEvent ON Agenda.Agenda_Id = AgendaEvent.FK_Agenda_Id INNER JOIN
							 Event ON AgendaEvent.FK_Event_Id = Event.Event_Id
	WHERE        (Person.[Name] = @Name)

	RETURN
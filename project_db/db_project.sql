CREATE DATABASE project_db;
GO;
CREATE PROC createAllTables
AS 
CREATE TABLE System_Users(
    username varchar(20),
    user_password varchar(20),
    CONSTRAINT PK_System_Users PRIMARY KEY (username)
);
CREATE TABLE Fan(
    national_id_number VARCHAR(20),
    fan_name varchar(20),
    birth_date DATETIME,
    fan_address varchar(20),
    fan_phone_number int,
    fan_status bit,
    username varchar(20),
    CONSTRAINT PK_Fan PRIMARY KEY (national_id_number),
    CONSTRAINT FK_username_fan FOREIGN KEY (username) 
    REFERENCES System_Users
    ON UPDATE CASCADE ON DELETE CASCADE
                  
);
CREATE TABLE Stadium (
	id int IDENTITY ,
	staduim_name varchar (20),
	staduim_location varchar(20),
	capacity int ,
	staduim_status bit ,
	CONSTRAINT pk_staduim_id PRIMARY KEY  (id),
);
CREATE TABLE Club (
	id int IDENTITY ,
	club_name varchar(20) ,
	club_location varchar(20),
	CONSTRAINT pk_club_id PRIMARY KEY (id),
);
CREATE TABLE Matches (
    id int IDENTITY ,
    start_time DATETIME ,
    end_time DATETIME,
    host_club_id int ,
    guest_club_id int ,
    staduim_id int ,
    CONSTRAINT pk_match_id PRIMARY KEY  (id),
    CONSTRAINT fk_host_club_id Foreign key (host_club_id) references Club
    ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_guest_cluub_id Foreign key (guest_club_id) references Club   ,
    CONSTRAINT fk_staduim_id Foreign key (staduim_id) references Stadium  
    ON UPDATE CASCADE ON DELETE SET NULL
);
CREATE TABLE Ticket (
    id int  identity ,
    ticket_status bit ,
    match_id int ,
    CONSTRAINT pk_ticket_id PRIMARY KEY  (ID),
    CONSTRAINT fk_match_id Foreign key (match_id) references Matches
    ON UPDATE CASCADE ON DELETE CASCADE

);	
CREATE TABLE Ticket_Buying_Transactions (
	fan_national_id VARCHAR(20),
	ticket_id int,
    CONSTRAINT PK_Ticket_Buying_Transactions PRIMARY KEY(fan_national_id,ticket_id),
	CONSTRAINT fk_fan_national_id Foreign key (fan_national_id) references Fan
    ON UPDATE CASCADE ON DELETE CASCADE  ,
	CONSTRAINT fk_ticket_id Foreign key (ticket_id) references Ticket
    ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE Stadium_Manager(
    id int IDENTITY,
    sm_name varchar(20),
    stadium_id int,
    username varchar(20),
    CONSTRAINT PK_Stadium_Manager PRIMARY KEY (id),
    CONSTRAINT FK_stadium_id FOREIGN KEY (stadium_id)
    REFERENCES Stadium     
    ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_username_sm FOREIGN KEY (username)
    REFERENCES System_Users  
    ON UPDATE CASCADE ON DELETE CASCADE

    
);

CREATE TABLE Club_Representative(
    id int IDENTITY,
    club_representative_name varchar(20),
    club_id int,
    username varchar(20),
    CONSTRAINT PK_Club_Representative PRIMARY KEY (id),
    CONSTRAINT FK_club_id FOREIGN KEY (club_id)
    REFERENCES Club
    ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_username_club FOREIGN KEY (username)
    REFERENCES System_Users  
    ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Sports_Association_Manager(
    id int IDENTITY,
    sam_name varchar(20),
    username varchar(20),
    CONSTRAINT PK_Sports_Association_Manager PRIMARY KEY(id),
    CONSTRAINT FK_username_sam FOREIGN KEY (username)
    REFERENCES System_Users  
    ON UPDATE CASCADE ON DELETE CASCADE
    
);
CREATE TABLE System_Admin(
    id int IDENTITY,
    sa_name varchar(20),
    username varchar(20),
    CONSTRAINT PK_System_Admin PRIMARY KEY(id),
    CONSTRAINT FK_username_sa FOREIGN KEY (username)
    REFERENCES System_Users  
    ON UPDATE CASCADE ON DELETE CASCADE
    
);

SELECT * FROM System_Users
CREATE TABLE Host_Request(
    id int IDENTITY,
    representative_id int,
    manager_id int,
    match_id int,
    request_status varchar(20),
    CONSTRAINT PK_Host_Request PRIMARY KEY(id),
    CONSTRAINT FK_representative_id FOREIGN KEY (representative_id)
    REFERENCES Club_Representative
    ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_manager_id FOREIGN KEY (manager_id)
    REFERENCES Stadium_Manager,
    CONSTRAINT FK_match_id_host FOREIGN KEY (match_id)
    REFERENCES Matches 
);
GO;


GO;
CREATE PROC dropAllTables 
AS
DROP TABLE Host_Request;
DROP TABLE System_Admin;
DROP TABLE Sports_Association_Manager;
DROP TABLE Club_Representative;
DROP TABLE Stadium_Manager;
DROP TABLE Ticket_Buying_Transactions;
DROP TABLE Ticket;
DROP TABLE Matches;
DROP TABLE Club;
DROP TABLE Stadium;
DROP TABLE Fan;
DROP TABLE System_Users;
GO



GO;
CREATE PROC dropAllProceduresFunctionsViews
AS
DROP PROC createAllTables;
DROP PROC dropAllTables;
DROP PROC clearAllTables;
DROP VIEW allAssocManagers;
DROP VIEW allClubRepresentatives;
DROP VIEW allStadiumManagers;
DROP VIEW allFans;
DROP VIEW allMatches;
DROP VIEW allTickets;
DROP VIEW allCLubs;
DROP VIEW allStadiums;
DROP VIEW allRequests;
DROP PROC addAssociationManager;
DROP PROC addNewMatch;
DROP VIEW clubsWithNoMatches;
DROP PROC deleteMatch;
DROP PROC deleteMatchesOnStadium;
DROP PROC addClub;
DROP PROC addTicket;
DROP PROC deleteClub;
DROP PROC addStadium;
DROP PROC deleteStadium;
DROP PROC blockFan;
DROP PROC unblockFan;
DROP PROC addRepresentative;
DROP FUNCTION viewAvailableStadiumsOn;
DROP PROC addHostRequest;
DROP FUNCTION allUnassignedMatches;
DROP PROC addStadiumManager;
DROP FUNCTION allPendingRequests;
DROP PROC acceptRequest;
DROP PROC rejectRequest;
DROP PROC addFan;
DROP FUNCTION upcomingMatchesOfClub;
DROP FUNCTION availableMatchesToAttend;
DROP PROC purchaseTicket;
DROP PROC updateMatchHost;
DROP VIEW matchesPerTeam;
DROP VIEW clubsNeverMatched;
DROP FUNCTION clubsNeverPlayed;
DROP FUNCTION matchWithHighestAttendance;
DROP FUNCTION matchesRankedByAttendance;
DROP FUNCTION requestsFromClub;
GO;




CREATE PROC clearAllTables
AS
delete from Host_Request;
delete from System_Admin;
delete from Sports_Association_Manager;
delete from Club_Representative;
delete from Stadium_Manager;
delete from Ticket_Buying_Transactions;
delete from Ticket;
delete from Matches;
delete from Club;
delete from Stadium;
delete from Fan;
delete from System_Users;
GO;


CREATE VIEW allAssocManagers 
AS 
SELECT s.username ,su.user_password , s.sam_name AS name FROM 
Sports_Association_Manager s INNER JOIN System_Users su ON s.username 
= su.username;
GO;
CREATE VIEW allClubRepresentatives
AS 
SELECT cr.username , su.user_password,  cr.club_representative_name AS name , c.club_name
FROM Club_Representative cr INNER JOIN Club c ON  cr.club_id = c.id
INNER JOIN System_Users su ON cr.username 
= su.username;
GO;
CREATE VIEW allStadiumManagers
AS 
SELECT sm.username , su.user_password , sm.sm_name AS name , s.staduim_name
FROM Stadium s INNER JOIN Stadium_Manager sm ON s.id = sm.stadium_id
INNER JOIN System_Users su ON sm.username 
= su.username;
GO;

CREATE VIEW allFans
AS 
SELECT f.username , su.user_password, 
f.fan_name , f.national_id_number , f.birth_date , f.fan_status
FROM Fan f INNER JOIN System_Users su ON f.username 
= su.username;
GO;

CREATE VIEW allMatches
AS 
SELECT host.club_name AS host_club_name,
guest.club_name AS guest_club_name,
M.start_time AS starting_time
FROM Matches M INNER JOIN Club host ON M.host_club_id = host.id
INNER JOIN Club guest ON M.guest_club_id = guest.id
GO;

CREATE  VIEW allTickets
AS 
SELECT host.club_name AS host_club_name,
guest.club_name AS guest_club_name,
s.staduim_name AS stadium_name,
M.start_time
FROM Ticket t INNER JOIN Matches M ON t.match_id = M.id
INNER JOIN Club host ON M.host_club_id = host.id
INNER JOIN Club guest ON M.guest_club_id = guest.id
INNER JOIN Stadium s ON M.staduim_id = s.id;
GO;

CREATE VIEW allCLubs
AS 
SELECT club_name AS name , club_location AS location 
FROM Club;
GO;

CREATE VIEW allStadiums
AS 
SELECT s.staduim_name AS name,
s.staduim_location AS location,
s.capacity AS capacity,
s.staduim_status AS stadium_status
FROM Stadium s;
GO;



CREATE VIEW allRequests
AS 
SELECT cr.username AS representative_username,
sm.username AS stadium_manager_username , hr.request_status
FROM Host_Request hr INNER JOIN Stadium_Manager sm 
ON hr.manager_id = sm.id
INNER JOIN Club_Representative cr ON hr.representative_id = cr.id;
GO;

CREATE PROC addAssociationManager
@name VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS 
IF @username IN (SELECT username 
FROM System_Users)
    PRINT 'THIS USERNAME ALREADY EXISTS !!!!'
ELSE
BEGIN
INSERT INTO System_Users VALUES(@username , @password); 
INSERT INTO Sports_Association_Manager VALUES (@name , @username);
END
GO;



CREATE PROC addNewMatch
@name_host VARCHAR(20),
@name_guest VARCHAR(20),
@start_time DATETIME,
@end_time DATETIME
AS 
DECLARE @id_host int
DECLARE @id_guest int
SELECT @id_host = C.id 
FROM Club C WHERE C.club_name = @name_host; 
SELECT @id_guest = C.id 
FROM Club C WHERE C.club_name = @name_guest;
INSERT INTO Matches VALUES (@start_time , @end_time , @id_host
, @id_guest , null);
GO;


CREATE VIEW clubsWithNoMatches
AS 
SELECT C.club_name FROM 
Club C WHERE NOT EXISTS (
    SELECT * FROM Matches M
    WHERE M.host_club_id = C.id OR M.guest_club_id = C.id
)
GO;

CREATE PROC deleteMatch
@host_name VARCHAR(20),
@guest_name VARCHAR(20)
AS 
DECLARE @id_host int
DECLARE @id_guest int
DECLARE @mid INT

SELECT @id_host = C.id 
FROM Club C WHERE C.club_name = @host_name; 
SELECT @id_guest = C.id 
FROM Club C WHERE C.club_name = @guest_name;


SELECT @mid = id 
FROM Matches 
WHERE host_club_id = @id_host AND guest_club_id = @id_guest; 

DELETE FROM Host_Request
WHERE match_id IN (
    SELECT id 
    FROM Matches 
    WHERE host_club_id = @id_host AND guest_club_id = @id_guest
)

DELETE FROM Ticket_Buying_Transactions
WHERE ticket_id IN (
    SELECT id 
    FROM Ticket
    WHERE match_id IN (
        SELECT id 
        FROM Matches 
        WHERE host_club_id = @id_host AND guest_club_id = @id_guest
    )
)

 
DELETE FROM Ticket
WHERE match_id IN (
    SELECT id 
    FROM Matches 
    WHERE host_club_id = @id_host AND guest_club_id = @id_guest
)


DELETE FROM Matches 
WHERE host_club_id = @id_host AND guest_club_id = @id_guest;

GO;
CREATE PROC deleteMatchesOnStadium
@stadium_name VARCHAR(20)
AS 
DECLARE @stadium_id int 
SELECT @stadium_id = s.id FROM 
Stadium s WHERE s.staduim_name = @stadium_name;

DELETE FROM Host_Request
WHERE match_id IN (
    SELECT id 
    FROM Matches 
    WHERE staduim_id = @stadium_id AND start_time > CURRENT_TIMESTAMP
)

DELETE FROM Ticket_Buying_Transactions
WHERE ticket_id IN (
    SELECT id 
    FROM Ticket
    WHERE match_id IN (
        SELECT id 
        FROM Matches 
        WHERE staduim_id = @stadium_id AND start_time > CURRENT_TIMESTAMP
    )
)

DELETE FROM Ticket
WHERE match_id IN (
    SELECT id 
    FROM Matches 
    WHERE staduim_id = @stadium_id AND start_time > CURRENT_TIMESTAMP
)


DELETE FROM Matches
WHERE staduim_id = @stadium_id AND start_time > CURRENT_TIMESTAMP
GO;


CREATE PROC addClub
@name VARCHAR(20),
@location VARCHAR(20)
AS 
INSERT INTO Club VALUES (@name , @location);
GO;

CREATE PROC addTicket
@host_name VARCHAR(20),
@guest_name VARCHAR(20),
@start_time DATETIME 
AS 
DECLARE @id int
DECLARE @id_host int
DECLARE @id_guest int
SELECT @id_host = C.id 
FROM Club C WHERE C.club_name = @host_name; 
SELECT @id_guest = C.id 
FROM Club C WHERE C.club_name = @guest_name;

SELECT @id = M.id FROM
Matches M 
WHERE @id_host = M.host_club_id AND
@id_guest = M.guest_club_id AND M.start_time = @start_time;

INSERT INTO Ticket VALUES(1 , @id);
GO;

CREATE PROC deleteClub
@name VARCHAR(20)
AS 
DECLARE @id int
DECLARE @rusername VARCHAR(20)

SELECT @id = id FROM Club 
WHERE club_name = @name;

SELECT @rusername = username FROM Club_Representative
WHERE club_id = @id

DELETE FROM Club 
WHERE club_name = @name


DELETE FROM Host_Request
WHERE match_id IN (
    SELECT id FROM Matches
    WHERE host_club_id = @id OR guest_club_id = @id
)

DELETE FROM Ticket_Buying_Transactions
WHERE ticket_id IN (
    SELECT id 
    FROM Ticket
    WHERE match_id IN (
        SELECT id 
        FROM Matches 
        WHERE host_club_id = @id OR guest_club_id = @id
    )
)

DELETE FROM Ticket
WHERE id in (SELECT t2.id
FROM Club C INNER JOIN Matches M ON C.id = m.host_club_id
INNER JOIN Ticket t2 ON t2.match_id = M.id
WHERE c.club_name = @name
) OR id in (SELECT t2.id
FROM Club C INNER JOIN Matches M ON C.id = m.guest_club_id
INNER JOIN Ticket t2 ON t2.match_id = M.id
WHERE c.club_name = @name)

DELETE FROM Matches 
WHERE host_club_id = @id OR guest_club_id = @id;

DELETE FROM System_Users WHERE username = @rusername
GO;

SELECT * FROM System_Admin


CREATE PROC addStadium
@name VARCHAR(20),
@location VARCHAR(20),
@capacity int 
AS 
INSERT INTO Stadium VALUES (@name , @location , @capacity , 1);
GO;


CREATE PROC deleteStadium
@name VARCHAR(20)
AS 
DECLARE @sid INT
DECLARE @smid INT
DECLARE @username VARCHAR(20)


SELECT @sid = id FROM 
Stadium WHERE staduim_name = @name

SELECT @username = username 
FROM Stadium_Manager 
WHERE stadium_id = @sid

SELECT @smid = id 
FROM Stadium_Manager 
WHERE stadium_id = @sid

DELETE from Host_Request 
WHERE manager_id = @smid;

DELETE FROM Ticket_Buying_Transactions
WHERE ticket_id IN (
    SELECT id 
    FROM Ticket
    WHERE id in (SELECT t2.id
    FROM Stadium s INNER JOIN Matches M ON s.id = m.staduim_id
    INNER JOIN Ticket t2 ON t2.match_id = M.id
    WHERE s.staduim_name = @name
)
)


DELETE FROM Ticket
WHERE id in (SELECT t2.id
FROM Stadium s INNER JOIN Matches M ON s.id = m.staduim_id
INNER JOIN Ticket t2 ON t2.match_id = M.id
WHERE s.staduim_name = @name
)

DELETE FROM System_Users 
WHERE username = @username

DELETE FROM Stadium WHERE staduim_name = @name;
GO;

CREATE PROC blockFan
@nid VARCHAR(20)
AS 
UPDATE Fan 
set fan_status = 0 WHERE national_id_number = @nid;
GO;



CREATE PROC unblockFan
@nid VARCHAR(20)
AS 
UPDATE Fan 
set fan_status = 1 WHERE national_id_number = @nid;
GO;

CREATE PROC addRepresentative
@name VARCHAR(20),
@clubname VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS 
declare @clubID int
SELECT @clubID = C.id 
FROM Club C WHERE C.club_name = @clubname; 
IF @username IN (SELECT username 
FROM System_Users)
    PRINT 'THIS USERNAME ALREADY EXISTS !!!!'
ELSE
BEGIN
INSERT INTO System_Users VALUES (@username , @password);
INSERT INTO Club_Representative VALUES(@name , @clubID , @username);
END
GO;


CREATE FUNCTION viewAvailableStadiumsOn
(@date DATETIME)
returns TABLE 
AS
RETURN  
    SELECT S.staduim_name , S.staduim_location , S.capacity 
    FROM Stadium S LEFT OUTER JOIN Matches M ON s.id = M.staduim_id
    WHERE S.staduim_status = '1' AND (M.id is null OR
    (M.id is NOT NULL AND NOT (@date >= M.start_time AND 
    @date <= M.end_time)));
GO;


CREATE PROC addHostRequest
@clubname VARCHAR(20),
@sname VARCHAR(20),
@start_time DATETIME 
AS 
declare @clubID int
SELECT @clubID = C.id 
FROM Club C WHERE C.club_name = @clubname; 

declare @sID int
SELECT @sID = S.id 
FROM Stadium S WHERE S.staduim_name = @sname; 

DECLARE @rID int 
SELECT @rID = R.id 
FROM Club_Representative R INNER JOIN Club C ON R.club_id = @clubID

DECLARE @mID int 
SELECT @mID = M.id 
FROM Stadium_Manager M INNER JOIN Stadium S  ON M.stadium_id = @sID

DECLARE @matchID int 
SELECT @matchID = M.id 
FROM Matches M INNER JOIN Club C ON C.id = M.host_club_id
WHERE M.host_club_id = @clubID 
AND M.start_time = @start_time;

INSERT INTO Host_Request VALUES(@rID , @mID , @matchID , 'unhandled');
GO;

CREATE FUNCTION allUnassignedMatches
(@name_club VARCHAR(20))
RETURNS TABLE
AS
RETURN (
    SELECT guest.club_name , M.start_time FROM 
    Club host INNER JOIN Matches M ON host.id = M.host_club_id
    INNER JOIN Club guest ON guest.id = M.guest_club_id
    WHERE M.staduim_id IS NULL AND host.club_name = @name_club
)   
GO;


CREATE PROC addStadiumManager
@name VARCHAR(20),
@sname VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20)
AS
DECLARE @sid int 
SELECT @sid = id FROM Stadium WHERE staduim_name = @sname
IF @username IN (SELECT username 
FROM System_Users)
    PRINT 'THIS USERNAME ALREADY EXISTS !!!!'
ELSE
BEGIN
INSERT INTO System_Users VALUES (@username , @password);
INSERT INTO Stadium_Manager VALUES (@name , @sid , @username);
END
GO;

CREATE FUNCTION allPendingRequests
(@username VARCHAR(20))
RETURNS TABLE 
AS 
RETURN (
    SELECT cr.club_representative_name , C.club_name , M.start_time
    FROM Stadium_Manager sm 
    INNER JOIN Host_Request hr ON sm.id = hr.manager_id
    INNER JOIN  Club_Representative cr ON hr.representative_id = cr.id
    INNER JOIN Matches M ON hr.match_id = M.id 
    INNER JOIN Club C ON M.guest_club_id = C.id
    WHERE sm.username = @username AND hr.request_status = 'unhandled'
)
GO;


CREATE PROC acceptRequest
@username VARCHAR(20),
@hostname VARCHAR(20),
@guestname VARCHAR(20),
@start_time DATETIME
AS
DECLARE @hostid INT
DECLARE @guestid INT
DECLARE @smid INT
DECLARE @rid INT
DECLARE @matchid INT
DECLARE @capacity INT
DECLARE @sid INT

SELECT @smid = id FROM 
Stadium_Manager WHERE username = @username;

SELECT @sid = stadium_id FROM
Stadium_Manager WHERE id = @smid

SELECT @hostid = id FROM 
Club WHERE club_name = @hostname

SELECT @guestid = id FROM 
Club WHERE club_name = @guestname;

SELECT @matchid = M.id 
FROM Matches M WHERE M.guest_club_id = @guestid AND M.host_club_id =
@hostid AND M.start_time = @start_time 

SELECT @rid = R.id 
FROM Club_Representative R INNER JOIN Club C ON R.club_id = C.id
WHERE C.id = @hostid

UPDATE Host_Request 
SET request_status = 'accepted' 
WHERE representative_id = @rid 
AND manager_id = @smid 
AND match_id = @matchid;



UPDATE Matches 
SET staduim_id = @sid
WHERE host_club_id = @hostid AND guest_club_id = @guestid
AND start_time = @start_time

SELECT @capacity = capacity 
FROM Matches M INNER JOIN Stadium s ON M.staduim_id = s.id
WHERE M.id = @matchid

DECLARE @i int = 0

WHILE @i < @capacity
BEGIN
    SET @i = @i + 1
    INSERT INTO Ticket VALUES(1 , @matchid)
END
GO;


CREATE PROC rejectRequest
@username VARCHAR(20),
@hostname VARCHAR(20),
@guestname VARCHAR(20),
@start_time DATETIME
AS
DECLARE @hostid INT
DECLARE @guestid INT
DECLARE @smid INT
DECLARE @rid INT
DECLARE @matchid INT

SELECT @smid = id FROM 
Stadium_Manager WHERE username = @username;

SELECT @hostid = id FROM 
Club WHERE club_name = @hostname

SELECT @guestid = id FROM 
Club WHERE club_name = @guestname;

SELECT @matchid = M.id 
FROM Matches M WHERE M.guest_club_id = @guestid AND M.host_club_id =
@hostid AND M.start_time = @start_time 

SELECT @rid = R.id 
FROM Club_Representative R INNER JOIN Club C ON R.club_id = C.id
WHERE C.id = @hostid

UPDATE Host_Request 
SET request_status = 'rejected' 
WHERE representative_id = @rid 
AND manager_id = @smid 
AND match_id = @matchid;
GO;  


CREATE PROC addFan
@name VARCHAR(20),
@username VARCHAR(20),
@password VARCHAR(20),
@nid VARCHAR(20),
@birthDate DATETIME,
@address VARCHAR(20),
@phonenumber VARCHAR(20)
AS
IF @username IN (SELECT username 
FROM System_Users)
    PRINT 'THIS USERNAME ALREADY EXISTS !!!!'
ELSE
BEGIN
INSERT INTO System_Users VALUES(@username , @password);
INSERT INTO Fan VALUES(@nid , @name , @birthDate , @address , 
@phonenumber , 1 , @username);
END
GO;

CREATE FUNCTION upcomingMatchesOfClub
(@clubname VARCHAR(20))
RETURNS TABLE
AS
RETURN (
    SELECT C.club_name AS given_club_name ,
    guest.club_name AS competing_club_name , 
    M.start_time , S.staduim_name
    FROM Club C INNER JOIN Matches M ON (C.id = M.host_club_id) 
    INNER JOIN Stadium S ON M.staduim_id = S.id
    INNER JOIN Club guest ON M.guest_club_id = guest.id
    WHERE C.club_name = @clubname AND M.start_time > CURRENT_TIMESTAMP

    UNION 

    SELECT C.club_name , host.club_name , M.start_time , S.staduim_name
    FROM Club C INNER JOIN Matches M ON (C.id = M.guest_club_id) 
    INNER JOIN Club host ON (host.id = M.host_club_id)
    INNER JOIN Stadium S ON M.staduim_id = S.id
    WHERE C.club_name = @clubname AND M.start_time > CURRENT_TIMESTAMP
)
GO;



CREATE FUNCTION availableMatchesToAttend
(@start_time DATETIME)
RETURNS TABLE 
AS 
RETURN (
    SELECT DISTINCT host.club_name AS host_club_name , 
    guest.club_name AS guest_club_name ,
    M.start_time , 
    S.staduim_name  FROM 
    Matches M INNER JOIN Ticket T ON M.id = T.match_id
    INNER JOIN Club host ON host.id = M.host_club_id 
    INNER JOIN Club guest ON guest.id = M.guest_club_id
    INNER JOIN Stadium s ON s.id = M.staduim_id
    WHERE T.ticket_status = 1 AND M.start_time >= @start_time
)
GO; 



CREATE PROC purchaseTicket
@nid VARCHAR(20),
@hostname VARCHAR(20),
@guestname VARCHAR(20),
@start_time DATETIME 
AS 
DECLARE @hostid int
DECLARE @guestid int 
DECLARE @matchid int
DECLARE @tid int

SELECT @hostid = id FROM Club 
WHERE club_name = @hostname

SELECT @guestid = id FROM Club 
WHERE club_name = @guestname

SELECT @matchid = id FROM Matches
WHERE host_club_id = @hostid AND guest_club_id = @guestid 
AND start_time = @start_time;

SELECT @tid = MIN(id) FROM Ticket t
WHERE t.match_id = @matchid AND t.ticket_status = 1

UPDATE Ticket SET ticket_status = 0 
WHERE id = @tid;

INSERT INTO Ticket_Buying_Transactions VALUES(@nid , @tid)

GO;

CREATE PROC updateMatchHost
@hostname VARCHAR(20),
@guestname VARCHAR(20),
@start_time DATETIME
AS
DECLARE @hostid int
DECLARE @guestid int 
DECLARE @matchid int

SELECT @hostid = id FROM Club 
WHERE club_name = @hostname

SELECT @guestid = id FROM Club 
WHERE club_name = @guestname

SELECT @matchid = id FROM Matches 
WHERE host_club_id = @hostid AND guest_club_id = @guestid 
AND start_time = @start_time;

UPDATE Matches SET host_club_id = @guestid, guest_club_id = @hostid 
WHERE id = @matchid
GO;

CREATE VIEW matchesPerTeam
AS
SELECT C.club_name , COUNT(*) AS Count_Matches
FROM Matches M INNER JOIN Club C ON (M.host_club_id = C.id 
OR M.guest_club_id = C.id)
WHERE M.start_time < CURRENT_TIMESTAMP
GROUP BY C.club_name
GO;

CREATE VIEW clubsNeverMatched
AS
SELECT C1.club_name AS first_club_name,
C2.club_name AS second_club_name
FROM Club C1 , Club C2
WHERE C1.id < C2.id AND NOT EXISTS (
    SELECT * 
    FROM Matches
    WHERE (host_club_id = C1.id AND guest_club_id = C2.id)
    OR (host_club_id = C2.id AND guest_club_id = C1.id)
)

GO;


CREATE FUNCTION clubsNeverPlayed
(@name VARCHAR(20))
RETURNS TABLE 
AS 
RETURN (
    SELECT * 
    FROM Club C 
    WHERE C.club_name <> @name AND NOT EXISTS(
        SELECT * FROM
        Club C2 INNER JOIN Matches M ON C2.id = M.host_club_id OR 
        C2.id = M.guest_club_id 
        WHERE C2.club_name = @name AND ( C.id = M.host_club_id OR 
        C.id = M.guest_club_id ) 
    )
)
GO;

CREATE FUNCTION matchWithHighestAttendance
()
RETURNS @result TABLE (
    hostname VARCHAR(20),
    guestname VARCHAR(20)
)
AS 
BEGIN
    WITH TEMP AS (
        SELECT host.club_name AS host_club_name 
        , guest.club_name AS guest_club_name
        , COUNT(T.id) AS tickets_Count
        FROM Matches M INNER JOIN Ticket T ON M.id = T.match_id
        INNER JOIN Club host ON host.id = M.host_club_id 
        INNER JOIN Club guest ON guest.id = M.guest_club_id
        WHERE T.ticket_status = 0
        GROUP BY host.club_name , guest.club_name

    )
    INSERT INTO @result SELECT host_club_name , guest_club_name
    FROM TEMP T1
    WHERE NOT EXISTS (SELECT * 
    FROM TEMP T2
    WHERE T2.tickets_Count > T1.tickets_Count);

    RETURN
END
GO;

CREATE FUNCTION matchesRankedByAttendance
()
RETURNS @result TABLE (
    hostname VARCHAR(20),
    guestname VARCHAR(20)
)
AS 
BEGIN
    WITH TEMP AS (
        SELECT host.club_name AS host_club_name 
        , guest.club_name AS guest_club_name
        , COUNT(T.id) AS tickets_Count
        FROM Matches M INNER JOIN Ticket T ON M.id = T.match_id
        INNER JOIN Club host ON host.id = M.host_club_id 
        INNER JOIN Club guest ON guest.id = M.guest_club_id
        WHERE T.ticket_status = 0
        GROUP BY host.club_name , guest.club_name
        ORDER BY  tickets_Count DESC OFFSET 0 ROWS 
    )
    INSERT INTO @result SELECT host_club_name , guest_club_name
    FROM TEMP;

    RETURN
END
GO;

CREATE FUNCTION requestsFromClub
(@sname VARCHAR(20),
@cname VARCHAR(20))
RETURNS @result TABLE(
    hostname VARCHAR(20),
    guestname VARCHAR(20)
)
AS
BEGIN
DECLARE @sid INT
DECLARE @hostid INT
DECLARE @rid INT
DECLARE @mid INT

SELECT @sid = id 
FROM Stadium S 
WHERE S.staduim_name = @sname;

SELECT @hostid = id 
FROM Club C
WHERE C.club_name = @cname;

SELECT @rid = id 
FROM Club_Representative cr
WHERE cr.club_id = @hostid;

SELECT @mid = id 
FROM Stadium_Manager sm 
WHERE sm.stadium_id = @sid;

INSERT INTO @result
SELECT host.club_name AS host_club_name 
, guest.club_name AS guest_club_name
FROM Host_Request hr INNER JOIN Matches M ON hr.match_id = M.id
INNER JOIN Club guest ON M.guest_club_id = guest.id
INNER JOIN Club host ON M.host_club_id = host.id
WHERE hr.manager_id = @mid AND hr.representative_id = @rid 
RETURN 
END
GO;



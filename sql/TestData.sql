USE Dem04DB

DELETE FROM EquipmentForRequest
DELETE FROM Equipment
DELETE FROM Requests
DBCC CHECKIDENT('Requests',RESEED,0)
DELETE FROM Workers
DBCC CHECKIDENT('Workers',RESEED,0)
DELETE FROM LOGINS
DELETE FROM Clients
DBCC CHECKIDENT('Clients',RESEED,0)
DELETE FROM Roles
DBCC CHECKIDENT('Roles',RESEED,0)

INSERT Roles
VALUES('WORKER'),('ADMIN')

INSERT LOGINS
VALUES
('ffde','1554'),
('ffeeff','15599551')

INSERT Clients
VALUES('Иван','Иванов','9878476295')

INSERT Workers VALUES
('Иван','Иванов','9878476295',1,'ffde'),
('Петр','Петров','5486781548',2,'ffeeff')

INSERT Requests VALUES
('01.01.2012','02.01.2012','ffffff','В работе',1,2,0),
('01.01.2012', NULL,'аааа','Принято',1,1,0),
('01.01.2012','02.01.2012','ДДДДДДДДДдвв','В работе',1,2,0)

select * from Requests
--INSERT Equipment Values
--INSERT EquipmentForRequest

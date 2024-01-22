USE Dem04DB

DELETE FROM Roles
INSERT Roles
VALUES('WORKER'),('ADMIN')
DELETE FROM LOGINS
INSERT LOGINS
VALUES
('ffde','1554'),
('ffeeff','15599551')

DELETE FROM Clients
INSERT Clients
VALUES('Иванов','Иван','9878476295')

DELETE FROM Workers
INSERT Workers VALUES
('Иванов','Иван','9878476295',1,'ffde'),
('Петров','Петр','5486781548',2,'ffeeff')


DELETE FROM Requests
DELETE FROM Equipment
DELETE FROM EquipmentForRequest







INSERT INTO Users (UserName, Email, PhoneNumber, PasswordHash, DateOfBirth, RegistrationDate, FirstName, LastName, City, Street, PostalCode, HouseNumber) VALUES ('DTroczyk', 'DTroczyk@gmail.com', '724462155', 'haslo', '1998-07-21', SYSDATETIME(), 'Dominik', 'Tracz', 'Œlêzany', null, '42-235', '30');
INSERT INTO Users (UserName, Email, PhoneNumber, PasswordHash, DateOfBirth, RegistrationDate, FirstName, LastName, City, Street, PostalCode, HouseNumber) VALUES ('Kowal', 'Kowal@gmail.com', '542381555', 'haslo', '1996-02-11', SYSDATETIME(), 'Jan', 'Kowalski', 'Kowalowo', 'Kowali', '48-345', '42');

Select * FROM Users

INSERT INTO Coffes (Name,Description) VALUES ('Latte', 'W³oski napój kawowy powstaj¹cy w wyniku wlania podgrzanego mleka do kawy espresso.');
INSERT INTO Coffes (Name,Description) VALUES ('Mocca', 'Jeden z wariantów kawy latte. Sk³ada siê z espresso, gor¹cego mleka oraz ciemnej lub mlecznej czekolady.');
INSERT INTO Coffes (Name,Description) VALUES ('Americana', 'Czarna kawa powsta³a z po³¹czenia wody i espresso.');
INSERT INTO Coffes (Name,Description) VALUES ('Flat white', 'Napój kawowy pochodz¹cy z Austraill lub Nowej Zelandii. Jest przygotowywany poprzez zalanie jednej lub dwóch porcji espresso spienionym mlekiem o jednorodnej, aksamitnej konsystencji.');
INSERT INTO Coffes (Name,Description) VALUES ('Espresso', 'Wywodzi siê z W³och, gdzie w 1901 Luigi Bezzera stworzy³ pierwszy ekspres do expresso. By³ on jednak konstrikcj¹ opart¹ na przep³ywie pary i wody, co prowadzi³o do smakowych zmian ekstraktu.');

Select * FROM Coffes

INSERT INTO Orders (OrderDate, ClientId, City, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'DTroczyk', 'Œlêzany', '30', 1, 0, '42-235');
INSERT INTO Orders (OrderDate, ClientId, City, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'DTroczyk', 'Œlêzany', '30', 2, 1, '42-235');
INSERT INTO Orders (OrderDate, ClientId, City, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'dtroczyk', 'Œlêzany', '30', 2, 1, '42-235');

INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'Kowal', 'Kowalowo', 'Kowali' ,'42', 3, 0, '48-345');
INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'Kowal', 'Kowalowo', 'Kowali' ,'42', 2, 1, '48-345');

Select * FROM Orders

INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (1, 1, 2, 0, 6, 10.99);
INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (1, 1, 2, 0, 7, 11.50);
INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (2, 2, 4, 0, 6, 12.99);
INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (3, 2, 4, 1, 7, 15.50);

INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (5, 3, 8, 1, 2, 42.99);
INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (4, 3, 5, 0, 7, 22.50);
INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (2, 4, 1, 0, 6, 8.99);
INSERT INTO OrderItems(CoffeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price) VALUES (3, 4, 2, 1, 7, 15.50);

Select * FROM OrderItems

/*

DROP TABLE Coffes
DROP TABLE OrderItems
DROP TABLE Orders
DROP TABLE Users
DROP TABLE __EFMigrationsHistory

*/
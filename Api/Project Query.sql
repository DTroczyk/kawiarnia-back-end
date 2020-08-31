INSERT INTO Users (UserName, Email, PhoneNumber, PasswordHash, DateOfBirth, RegistrationDate, FirstName, LastName, City, Street, PostalCode, HouseNumber, IsVerifiedEmail, Salt) VALUES ('DTroczyk', 'DTroczyk@gmail.com', '724462155', 'FtLaz60bkEEw8NRm2eeZODjU8Do=', '1998-07-21', SYSDATETIME(), 'Dominik', 'Tracz', 'Œlêzany', '', '42-235', '30', 0, 'A18RkbAQ/aFkaoyRO/cGtQ==');

Select * FROM Users

INSERT INTO Coffees (Name,Description,Price) VALUES ('Latte', 'W³oski napój kawowy powstaj¹cy w wyniku wlania podgrzanego mleka do kawy espresso.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('Mocca', 'Jeden z wariantów kawy latte. Sk³ada siê z espresso, gor¹cego mleka oraz ciemnej lub mlecznej czekolady.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('Americana', 'Czarna kawa powsta³a z po³¹czenia wody i espresso.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('FlatWhite', 'Napój kawowy pochodz¹cy z Austraill lub Nowej Zelandii. Jest przygotowywany poprzez zalanie jednej lub dwóch porcji espresso spienionym mlekiem o jednorodnej, aksamitnej konsystencji.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('Espresso', 'Wywodzi siê z W³och, gdzie w 1901 Luigi Bezzera stworzy³ pierwszy ekspres do expresso. By³ on jednak konstrukcj¹ opart¹ na przep³ywie pary i wody, co prowadzi³o do smakowych zmian ekstraktu.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('YourOwn', 'Do it yourself.',100);

Select * FROM Coffees

INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'DTroczyk', 'Œlêzany', '', '30', blik, 0, '42-235');
INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'DTroczyk', 'Œlêzany', '', '30', visa, 1, '42-235');
INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'dtroczyk', 'Œlêzany', '', '30', mastercard, 1, '42-235');

Select * FROM Orders

INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Latte', 1, 2, 0, 6, 10.99, 1);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Latte', 1, 2, 0, 7, 11.50, 1);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Mocca', 2, 4, 0, 6, 12.99, 3);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Americana', 2, 4, 1, 7, 15.50, 3);

Select * FROM OrderItems

/*

DROP TABLE OrderItems
DROP TABLE Orders
DROP TABLE Users
DROP TABLE Coffees
DROP TABLE __EFMigrationsHistory

*/
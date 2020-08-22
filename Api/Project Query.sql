INSERT INTO Users (UserName, Email, PhoneNumber, PasswordHash, DateOfBirth, RegistrationDate, FirstName, LastName, City, Street, PostalCode, HouseNumber, IsVerifiedEmail) VALUES ('DTroczyk', 'DTroczyk@gmail.com', '724462155', 'haslo', '1998-07-21', SYSDATETIME(), 'Dominik', 'Tracz', '�l�zany', '', '42-235', '30', 1);
INSERT INTO Users (UserName, Email, PhoneNumber, PasswordHash, DateOfBirth, RegistrationDate, FirstName, LastName, City, Street, PostalCode, HouseNumber, IsVerifiedEmail) VALUES ('Kowal', 'Kowal@gmail.com', '542381555', 'haslo', '1996-02-11', SYSDATETIME(), 'Jan', 'Kowalski', 'Kowalowo', 'Kowali', '48-345', '42', 0);

Select * FROM Users

INSERT INTO Coffees (Name,Description,Price) VALUES ('Latte', 'W�oski nap�j kawowy powstaj�cy w wyniku wlania podgrzanego mleka do kawy espresso.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('Mocca', 'Jeden z wariant�w kawy latte. Sk�ada si� z espresso, gor�cego mleka oraz ciemnej lub mlecznej czekolady.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('Americana', 'Czarna kawa powsta�a z po��czenia wody i espresso.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('FlatWhite', 'Nap�j kawowy pochodz�cy z Austraill lub Nowej Zelandii. Jest przygotowywany poprzez zalanie jednej lub dw�ch porcji espresso spienionym mlekiem o jednorodnej, aksamitnej konsystencji.',100);
INSERT INTO Coffees (Name,Description,Price) VALUES ('Espresso', 'Wywodzi si� z W�och, gdzie w 1901 Luigi Bezzera stworzy� pierwszy ekspres do expresso. By� on jednak konstrukcj� opart� na przep�ywie pary i wody, co prowadzi�o do smakowych zmian ekstraktu.',100);

Select * FROM Coffees

INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'DTroczyk', '�l�zany', '', '30', 1, 0, '42-235');
INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'DTroczyk', '�l�zany', '', '30', 2, 1, '42-235');
INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'dtroczyk', '�l�zany', '', '30', 2, 1, '42-235');

INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'Kowal', 'Kowalowo', 'Kowali' ,'42', 3, 0, '48-345');
INSERT INTO Orders (OrderDate, ClientId, City, Street, HouseNumber, PaymentMethod, IsPaymentCompleted, PostalCode) VALUES (SYSDATETIME(), 'Kowal', 'Kowalowo', 'Kowali' ,'42', 2, 1, '48-345');

Select * FROM Orders

INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Latte', 1, 2, 0, 6, 10.99, 1);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Latte', 1, 2, 0, 7, 11.50, 1);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Mocca', 2, 4, 0, 6, 12.99, 3);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Americana', 2, 4, 1, 7, 15.50, 3);

INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Espresso', 3, 8, 1, 2, 42.99, 3);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('FlatWhite', 3, 5, 0, 7, 22.50, 3);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Mocca', 4, 1, 0, 6, 8.99, 1);
INSERT INTO OrderItems(CoffeeId, OrderId, EspressoCount, IsContainChocolate, MilkCount, Price, PaymentStatus) VALUES ('Americana', 4, 2, 1, 7, 15.50, 1);

Select * FROM OrderItems

/*

DROP TABLE OrderItems
DROP TABLE Orders
DROP TABLE Users
DROP TABLE Coffees
DROP TABLE __EFMigrationsHistory

*/
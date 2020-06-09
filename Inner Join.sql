SELECT Orders.OrderId,(SELECT CONCAT(Users.UserName, ' ', Users.UserSurname) FROM Users WHERE UserId = Orders.UserId) as UserData, Photo,NumbCopies, Seveces.SName,(SELECT Price FROM Seveces WHERE Seveces.serviceId = Order_Services.serviceID)*NumbCopies as Price, Statuses.Name as Status FROM Orders
INNER JOIN Order_Services ON Order_Services.orderID = Orders.OrderId
INNER JOIN Statuses ON Statuses.Id = Orders.StatusId
INNER JOIN Seveces ON Seveces.serviceId = Order_Services.serviceID;

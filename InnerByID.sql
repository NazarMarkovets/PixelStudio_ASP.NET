select 
	
	Orders.TotalPrice,
	CONCAT(Users.UserName, ' ', Users.UserSurname)UserData,
	Statuses.Name,
	Order_Services.Photo
FROM Orders
inner join Order_Services ON Orders.OrderId = Order_Services.orderID
Inner join Users ON Users.UserId = Orders.UserId
Inner join Statuses ON Statuses.Id = Orders.StatusId
where OrderId = 10;
SELECT 
	Seveces.SName,
	SUM((Seveces.Price * Order_Services.NumbCopies)) as price

FROM 
	Order_Services
	inner join 
	Orders on Order_Services.orderID = Orders.OrderId
	inner join 
	Seveces on Seveces.serviceId = Order_Services.serviceID


GROUP BY Seveces.SName
ORDER BY price;

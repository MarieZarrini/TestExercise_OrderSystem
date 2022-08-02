using OrderSystem;

var orderItem = new OrderItem(2, "Whatever");
var order = new Order(1, orderItem);

order.AddOrderItem(orderItem);
order.SetOrderStateToFinalized();

order.AddOrderItem(orderItem);
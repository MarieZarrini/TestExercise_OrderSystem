using System.Collections.Generic;

namespace OrderSystem.Tests.Unit.Factories
{
	public class OrderFactory
	{
		private readonly int _userId;
		private List<OrderItem> _orderItems = new();
		OrderItem orderItem = new OrderItemFactory().GetSomeOrderItem();

		public OrderFactory()
		{
			_userId = 1;
			_orderItems.Add(orderItem);
		}


		public Order GetSomeOrder()
		{
			return new Order(_userId, _orderItems);
		}

		public Order GetSomeOrder(int userId)
		{
			return new Order(userId, _orderItems);
		}

		public Order GetSomeOrder(OrderItem orderItem)
		{
			_orderItems = new List<OrderItem>() { orderItem };
			return new Order(_userId, _orderItems);
		}

		public Order GetSomeOrder(List<OrderItem> orderItems)
		{
			return new Order(_userId, orderItems);
		}

		public Order GetSomeOrder(int userId, OrderItem orderItem)
		{
			_orderItems = new List<OrderItem>() { orderItem };
			return new Order(userId, _orderItems);
		}
	}
}

using System.Collections.Generic;

namespace OrderSystem.Tests.Unit
{
	public class OrderBuilder
	{
		private int _userId;
		private readonly List<OrderItem> _orderItems = new();
		OrderItem OrderItem = new OrderItemBuilder().Build();

		public OrderBuilder()
		{
			_userId = 1;
			_orderItems.Add(OrderItem);
		}


		public OrderBuilder WithUserId(int userId)
		{
			_userId = userId;
			return this;
		}

		public OrderBuilder WithOrderItems(OrderItem orderItem)
		{
			_orderItems.Add(orderItem);
			return this;
		}

		public OrderBuilder WithDefaultOrderItem(OrderItem orderItem)
		{
			OrderItem = orderItem;
			return this;
		}

		public Order Build()
		{
			var order = new Order(_userId, OrderItem);
			return order;
		}
	}
}

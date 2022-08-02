using System.Collections.Generic;

namespace OrderSystem.Tests.Unit
{
	public class OrderBuilder
	{
		private int _userId;
		private List<OrderItem> _orderItems = new();
		OrderItem orderItem = new OrderItemBuilder().Build();

		public OrderBuilder()
		{
			_userId = 1;
			_orderItems.Add(orderItem);
		}


		public OrderBuilder WithUserId(int userId)
		{
			_userId = userId;
			return this;
		}

		public OrderBuilder WithOrderItems(List<OrderItem> orderItems)
		{
			_orderItems = orderItems;
			return this;
		}

		public OrderBuilder WithDefaultOrderItem(OrderItem orderItem)
		{
			this.orderItem = orderItem;
			return this;
		}

		public Order Build()
		{
			var order = new Order(_userId, _orderItems);
			return order;
		}
	}
}

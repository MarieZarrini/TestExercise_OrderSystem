using NSubstitute;
using System;
using System.Collections.Generic;

namespace OrderSystem.Tests.Unit
{
	public class OrderBuilder
	{
		private int _userId;
		private List<OrderItem> _orderItems = new();
		OrderItem orderItem = new OrderItemBuilder().Build();
		private IMessageService _messageServiceMock;
		private IOrderRepository _orderRepositoryMock;

		public OrderBuilder()
		{
			_userId = 1;
			_orderItems.Add(orderItem);
			_messageServiceMock = Substitute.For<IMessageService>();
			_orderRepositoryMock = Substitute.For<IOrderRepository>();
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

		public OrderBuilder WithOrderItem(OrderItem orderItem)
		{
			this.orderItem = orderItem;
			return this;
		}

		public OrderBuilder WithMessageService(IMessageService messageService)
		{
			_messageServiceMock = messageService;
			return this;
		}
		public OrderBuilder WithOrderRepository(IOrderRepository orderRepository)
		{
			_orderRepositoryMock = orderRepository;
			return this;
		}

		public Order Build()
		{
			var order = new Order(_userId, _orderItems, _messageServiceMock, _orderRepositoryMock);
			return order;
		}

	}
}

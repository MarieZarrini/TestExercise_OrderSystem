using NSubstitute;
using System.Collections.Generic;

namespace OrderSystem.Tests.Unit.Factories
{
	public class OrderFactory
	{
		private readonly int _userId;
		private List<OrderItem> _orderItems = new();
		OrderItem orderItem = new OrderItemFactory().GetSomeOrderItem();
		private readonly IMessageService _messageServiceMock;
		private readonly IOrderRepository _orderRepositoryMock;

		public OrderFactory()
		{
			_userId = 1;
			_orderItems.Add(orderItem);
			_messageServiceMock = Substitute.For<IMessageService>();
			_orderRepositoryMock = Substitute.For<IOrderRepository>();
		}


		public Order GetSomeOrder()
		{
			return new Order(_userId, _orderItems, _messageServiceMock, _orderRepositoryMock);
		}

		public Order GetSomeOrder(int userId)
		{
			return new Order(userId, _orderItems, _messageServiceMock, _orderRepositoryMock);
		}

		public Order GetSomeOrder(OrderItem orderItem)
		{
			_orderItems = new List<OrderItem>() { orderItem };
			return new Order(_userId, _orderItems, _messageServiceMock, _orderRepositoryMock);
		}

		public Order GetSomeOrder(List<OrderItem> orderItems)
		{
			return new Order(_userId, orderItems, _messageServiceMock, _orderRepositoryMock);
		}

		public Order GetSomeOrder(int userId, OrderItem orderItem)
		{
			_orderItems = new List<OrderItem>() { orderItem };
			return new Order(userId, _orderItems, _messageServiceMock, _orderRepositoryMock);
		}
	}
}

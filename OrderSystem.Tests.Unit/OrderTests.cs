using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OrderSystem.BusinessExceptions;
using OrderSystem.Tests.Unit.Factories;
using System.Collections.Generic;
using Xunit;

namespace OrderSystem.Tests.Unit
{
	public class OrderTests
	{
		private readonly OrderBuilder _orderBuilder;
		private readonly OrderFactory _orderFactory;
		private readonly OrderItemFactory _orderItemFactory;

		public OrderTests()
		{
			_orderBuilder = new OrderBuilder();
			_orderFactory = new OrderFactory();
			_orderItemFactory = new OrderItemFactory();
		}



		#region Happy Tests

		[Fact]
		public void Order_Should_Send_Message_At_Creation_Time()
		{
			const string message = "New order just submited.";
			const string phoneNumber = "0912";
			var messageServiceMock = Substitute.For<IMessageService>();
			_orderBuilder.WithMessageService(messageServiceMock).Build();

			messageServiceMock.Received().Send(message, phoneNumber);
		}

		[Fact]
		public void Order_Should_Create_Correctly()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem();
			//var order = _orderFactory.GetSomeOrder(1, orderItem);
			var order = _orderBuilder.WithUserId(1).WithOrderItem(orderItem).Build();
			List<OrderItem> expectedOrderItems = new() { orderItem };

			order.UserId.Should().Be(1);
			order.OrderItems.Should().BeEquivalentTo(expectedOrderItems);
		}

		[Fact]
		public void Order_State_Should_Be_Created_When_New_Order_Create()
		{
			var order = _orderBuilder.Build();

			order.State.Should().Be(OrderState.Created);
		}

		[Fact]
		public void Order_State_Should_Change_To_Finalized_After_Calling_Finalized()
		{
			var order = _orderBuilder.Build();

			order.Finalized();

			order.State.Should().Be(OrderState.Finalized);
		}

		[Fact]
		public void Order_State_Should_Change_To_Shipped_After_Calling_Shipped()
		{
			var order = _orderBuilder.Build();

			order.Finalized();
			order.Shipped();

			order.State.Should().Be(OrderState.shipped);
		}

		[Fact]
		public void OrderItem_Should_Add_To_OrderItems_List_Correctly()
		{
			var orderItem1 = _orderItemFactory.GetSomeOrderItem(1, "laptop");
			var orderItem2 = _orderItemFactory.GetSomeOrderItem(2, "phone");
			var order = _orderFactory.GetSomeOrder(orderItem1);
			List<OrderItem> expectedOrderItems = new()
			{
				orderItem1, orderItem2
			};

			order.AddOrderItem(orderItem2);

			order.OrderItems.Should().BeEquivalentTo(expectedOrderItems);
		}

		[Fact]
		public void OrderItem_Should_Remove_From_OrderItems_List_Correctly()
		{
			//arrange
			var orderItem1 = _orderItemFactory.GetSomeOrderItem();
			var orderItem2 = _orderItemFactory.GetSomeOrderItem(2, "phone");
			List<OrderItem> orderItems = new()
			{
				orderItem1,
				orderItem2
			};

			var order = _orderFactory.GetSomeOrder(orderItems);

			List<OrderItem> expectedOrderItems = new() { orderItem2 };

			//act
			order.RemoveOrderItem(orderItem1);

			//assert
			order.OrderItems.Should().BeEquivalentTo(expectedOrderItems);
		}

		#endregion

		#region Unhappy Tests

		[Fact]
		public void Order_Should_Throw_NullOrderException_If_Order_Does_Not_Exists()
		{
			const int orderId = 1;
			var orderRepositoryStub = Substitute.For<IOrderRepository>();
			orderRepositoryStub.GetById(orderId).ReturnsNull();
			var order = _orderBuilder.WithOrderRepository(orderRepositoryStub).Build();

			var getOrderById = () => order.GetOrderById(orderId);

			getOrderById.Should().Throw<NullOrderException>();
		}

		[Fact]
		public void Order_Should_Throw_NullOrEmptyOrderItemsException_If_OrderItems_Is_Empty()
		{
			var orderItems = new List<OrderItem>();
			var order = () => _orderFactory.GetSomeOrder(orderItems);

			order.Should().Throw<NullOrEmptyOrderItemsException>();
		}

		[Fact]
		public void Order_Should_Throw_NullOrEmptyOrderItemsException_If_OrderItems_Is_Nul()
		{
			var order = () => _orderFactory.GetSomeOrder(orderItems: null);

			order.Should().Throw<NullOrEmptyOrderItemsException>();
		}

		[Fact]
		public void Order_Should_Throw_SetStateToFinalizedException_When_Modify_State_From_Shipped_To_Finalized()
		{
			var order = _orderBuilder.Build();
			order.Finalized();
			order.Shipped();

			var finalized = () => order.Finalized();

			finalized.Should().Throw<SetStateToFinalizedException>();
		}

		[Fact]
		public void Order_Should_Throw_SetStateToShippedException_When_Modify_State_From_Created_To_Shipped()
		{
			var order = _orderFactory.GetSomeOrder();

			var shipped = () => order.Shipped();

			shipped.Should().Throw<SetStateToShippedException>();
		}

		[Fact]
		public void Order_Should_Throw_NullOrderItemException_When_Add_Null_OrderItem_To_OrderItem_List()
		{
			var order = _orderBuilder.Build();

			var addOrderItem = () => order.AddOrderItem(null);

			addOrderItem.Should().Throw<NullOrderItemException>();
		}

		[Fact]
		public void Order_Should_Throw_InvalidOrderStateException_When_State_Is_Finalized_And_Try_To_Add_To_OrderItem_List()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem();
			var order = _orderBuilder.Build();
			order.Finalized();

			var addOrderItem = () => order.AddOrderItem(orderItem);

			addOrderItem.Should().Throw<InvalidOrderStateException>();
		}

		[Fact]
		public void Order_Should_Throw_InvalidOrderStateException_When_State_Is_Shipped_And_Try_To_Add_To_OrderItem_List()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem();
			var order = _orderBuilder.Build();
			order.Finalized();
			order.Shipped();

			var addOrderItem = () => order.AddOrderItem(orderItem);

			addOrderItem.Should().Throw<InvalidOrderStateException>();
		}

		[Fact]
		public void Order_Should_Throw_InvalidOrderStateException_When_State_Is_Finalized_And_Try_To_Remove_From_OrderItem_List()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem();
			var order = _orderBuilder.Build();
			order.Finalized();

			var removeOrderItem = () => order.RemoveOrderItem(orderItem);

			removeOrderItem.Should().Throw<InvalidOrderStateException>();
		}

		[Fact]
		public void Order_Should_Throw_InvalidOrderStateException_When_State_Is_Shipped_And_Try_To_Remove_From_OrderItem_List()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem();
			var order = _orderBuilder.Build();
			order.Finalized();
			order.Shipped();

			var removeOrderItem = () => order.RemoveOrderItem(orderItem);

			removeOrderItem.Should().Throw<InvalidOrderStateException>();
		}

		[Fact]
		public void RemoveOrderItem_Should_Throw_NullOrEmptyOrderItemsException_When_OrderItems_Count_Equals_To_One()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem();
			var order = _orderBuilder.Build();

			var removeOrderItem = () => order.RemoveOrderItem(orderItem);

			removeOrderItem.Should().Throw<NullOrEmptyOrderItemsException>();
		}

		#endregion
	}
}

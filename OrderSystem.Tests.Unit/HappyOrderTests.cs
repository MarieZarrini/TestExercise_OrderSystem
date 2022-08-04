using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace OrderSystem.Tests.Unit
{
	public class HappyOrderTests
	{
		[Fact]
		public void Order_Should_Create_Correctly()
		{
			//arrange
			var orderItem = new OrderItemBuilder().Build();

			var orderBuilder = new OrderBuilder()
				.WithUserId(1)
				.WithOrderItem(orderItem);
			var order = orderBuilder.Build();

			List<OrderItem> expectedOrderItems = new() { orderItem };

			//assert
			Assert.Equal(1, order.UserId);
			order.OrderItems.Should().BeEquivalentTo(expectedOrderItems);
		}

		[Fact]
		public void Order_State_Should_Be_Created_When_New_Order_Create()
		{
			var order = new OrderBuilder().Build();

			order.State.Should().Be(OrderState.Created);
		}

		[Fact]
		public void Order_State_Should_Change_To_Finalized_After_Calling_Finalized()
		{
			var order = new OrderBuilder().Build();

			order.Finalized();

			order.State.Should().Be(OrderState.Finalized);
		}

		[Fact]
		public void Order_State_Should_Change_To_Shipped_After_Calling_Shipped()
		{
			var order = new OrderBuilder().Build();

			order.Finalized();
			order.Shipped();

			order.State.Should().Be(OrderState.shipped);
		}

		[Fact]
		public void OrderItem_Should_Add_To_OrderItems_List_Correctly()
		{
			//arrange
			var orderItem = new OrderItemBuilder()
				.WithCount(2)
				.WithName("laptop")
				.Build();
			var order = new OrderBuilder()
				.WithOrderItem(orderItem)
				.Build();

			List<OrderItem> expectedOrderItems = new()
			{
				orderItem,
				orderItem
			};

			//act
			order.AddOrderItem(orderItem);

			//assert
			order.OrderItems.Should().BeEquivalentTo(expectedOrderItems);
		}

		[Fact]
		public void OrderItem_Should_Remove_From_OrderItems_List_Correctly()
		{
			//arrange
			var firstOrderItem = new OrderItemBuilder().Build();
			var secondOrderItem = new OrderItemBuilder().WithCount(2).WithName("mobile").Build();
			var orderItems = new List<OrderItem>
			{
				firstOrderItem, secondOrderItem
			};
			var order = new OrderBuilder().WithOrderItems(orderItems).Build();

			List<OrderItem> expectedOrderItems = new()
			{
				secondOrderItem
			};

			//act
			order.RemoveOrderItem(firstOrderItem);

			//assert
			order.OrderItems.Should().BeEquivalentTo(expectedOrderItems);
		}
	}
}

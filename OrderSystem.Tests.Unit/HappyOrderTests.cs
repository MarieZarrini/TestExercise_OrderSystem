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
			var orderItem = new OrderItemBuilder().Build();
			var orderBuilder = new OrderBuilder()
				.WithUserId(1)
				.WithDefaultOrderItem(orderItem);

			var order = orderBuilder.Build();

			Assert.Equal(1, order.UserId);

			List<OrderItem> orderItems = new() { orderItem };
			Assert.Equal(orderItems, order.OrderItems);
		}

		[Fact]
		public void Order_State_Should_Be_Created_When_New_Order_Create()
		{
			var order = new OrderBuilder().Build();

			order.State.Should().Be(OrderState.Created);
		}

		[Fact]
		public void Order_State_Should_Change_To_Finalized_After_Calling_SetOrderStateToFinalized()
		{
			var order = new OrderBuilder().Build();

			order.SetOrderStateToFinalized();

			order.State.Should().Be(OrderState.Finalized);
		}

		[Fact]
		public void Order_State_Should_Change_To_Shipped_After_Calling_SetOrderStateToShipped()
		{
			var order = new OrderBuilder().Build();

			order.SetOrderStateToFinalized();
			order.SetOrderStateToShipped();

			order.State.Should().Be(OrderState.shipped);
		}

		[Fact]
		public void OrderItem_Should_Add_To_OrderItems_List_Correctly()
		{
			var orderItem = new OrderItemBuilder()
				.WithCount(2)
				.WithName("test")
				.Build();

			var order = new OrderBuilder()
				.WithDefaultOrderItem(orderItem)
				.Build();

			order.AddOrderItem(orderItem);

			order.OrderItems.Should().HaveCount(2);
			order.OrderItems[1].Should().Be(orderItem);
		}

		[Fact]
		public void OrderItem_Should_Remove_From_OrderItems_List_Correctly()
		{
			var orderItem = new OrderItemBuilder().Build();
			var orderItems = new List<OrderItem>
			{
				orderItem,
				orderItem
			};

			var order = new OrderBuilder().WithOrderItems(orderItems).Build();

			order.RemoveOrderItem(orderItem);

			order.OrderItems.Should().HaveCount(1);
		}
	}
}

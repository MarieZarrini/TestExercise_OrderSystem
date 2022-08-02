using FluentAssertions;
using OrderSystem.BusinessExceptions;
using Xunit;

namespace OrderSystem.Tests.Unit
{
	public class UnhappyOrderTests
	{
		[Fact]
		public void Order_Should_Throw_NullOrderItemException_If_OrderItem_Is_Null()
		{
			var orderBuilder = new OrderBuilder().WithDefaultOrderItem(null);

			var order = () => orderBuilder.Build();

			order.Should().Throw<NullOrderItemException>();
		}

		[Fact]
		public void Order_Should_Throw_SetStateToFinalizedException_When_Modify_State_From_Shipped_To_Finalized()
		{
			var order = new OrderBuilder().Build();
			order.SetOrderStateToFinalized();
			order.SetOrderStateToShipped();

			var setOrderStateToFinalized = () => order.SetOrderStateToFinalized();

			setOrderStateToFinalized.Should().Throw<SetStateToFinalizedException>();
		}

		[Fact]
		public void Order_Should_Throw_SetStateToShippedException_When_Modify_State_From_Created_To_Shipped()
		{
			var orderBuilder = new OrderBuilder();
			var order = orderBuilder.Build();

			var setOrderStateToShipped = () => order.SetOrderStateToShipped();

			setOrderStateToShipped.Should().Throw<SetStateToShippedException>();
		}

		[Fact]
		public void Order_Should_Throw_NullOrderItemException_When_Add_Null_OrderItem_To_OrderItem_List()
		{
			var order = new OrderBuilder().Build();

			var addOrderItem = () => order.AddOrderItem(null);

			addOrderItem.Should().Throw<NullOrderItemException>();
		}

		[Fact]
		public void Order_Should_Throw_InvalidOrderStateException_When_State_Is_Finalized_And_Try_To_Add_To_OrderItem_List()
		{
			var orderItem = new OrderItemBuilder().Build();
			var order = new OrderBuilder().Build();
			order.SetOrderStateToFinalized();

			var addOrderItem = () => order.AddOrderItem(orderItem);

			addOrderItem.Should().Throw<InvalidOrderStateException>();
		}

		[Fact]
		public void Order_Should_Throw_InvalidOrderStateException_When_State_Is_Shipped_And_Try_To_Add_To_OrderItem_List()
		{
			var orderItem = new OrderItemBuilder().Build();
			var order = new OrderBuilder().Build();

			order.SetOrderStateToFinalized();
			order.SetOrderStateToShipped();

			var addOrderItem = () => order.AddOrderItem(orderItem);

			addOrderItem.Should().Throw<InvalidOrderStateException>();
		}
	}
}

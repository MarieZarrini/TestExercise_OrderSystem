using FluentAssertions;
using OrderSystem.BusinessExceptions;
using OrderSystem.Tests.Unit.Factories;
using Xunit;

namespace OrderSystem.Tests.Unit
{
	public class OrderItemTests
	{
		private readonly OrderItemFactory _orderItemFactory;

		public OrderItemTests()
		{
			_orderItemFactory = new OrderItemFactory();
		}


		[Theory]
		[InlineData(-2)]
		[InlineData(0)]
		[InlineData(5)]
		public void OrderItem_Should_Throw_InvalidCountException_With_Invalid_Count(int count)
		{
			var orderItem = () => _orderItemFactory.GetSomeOrderItem(count);

			orderItem.Should().Throw<InvalidCountException>();
		}

		[Fact]
		public void OrderItem_Should_Be_Created_Correctly()
		{
			var orderItem = _orderItemFactory.GetSomeOrderItem(1, "laptop");

			Assert.Equal(1, orderItem.Count);
			Assert.Equal("laptop", orderItem.Name);
		}
	}
}
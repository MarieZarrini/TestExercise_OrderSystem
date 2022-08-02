using FluentAssertions;
using OrderSystem.BusinessExceptions;
using Xunit;

namespace OrderSystem.Tests.Unit
{
	public class OrderItemTests
	{
		[Theory]
		[InlineData(-2)]
		[InlineData(0)]
		[InlineData(5)]
		public void OrderItem_Should_Throw_InvalidCountException_With_Invalid_Count(int count)
		{
			var orderItemBuilder = new OrderItemBuilder().WithCount(count);

			var orderItem = () => orderItemBuilder.Build();

			orderItem.Should().Throw<InvalidCountException>();
		}

		[Fact]
		public void OrderItem_Should_Be_Created_Correctly()
		{
			var orderItemBuilder = new OrderItemBuilder()
				.WithCount(1)
				.WithName("test");

			var orderItem = orderItemBuilder.Build();

			Assert.Equal(1, orderItem.Count);
			Assert.Equal("test", orderItem.Name);
		}
	}
}
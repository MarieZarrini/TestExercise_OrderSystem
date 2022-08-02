namespace OrderSystem.Tests.Unit
{
	public class OrderItemBuilder
	{
		private int _count;
		private string _name;

		public OrderItemBuilder()
		{
			_count = 2;
			_name = "test";
		}


		public OrderItemBuilder WithCount(int count)
		{
			_count = count;
			return this;
		}

		public OrderItemBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public OrderItem Build()
		{
			var orderItem = new OrderItem(_count, _name);
			return orderItem;
		}
	}
}

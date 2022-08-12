namespace OrderSystem.Tests.Unit.Factories
{
	public class OrderItemFactory
	{
		private readonly int _count;
		private readonly string _name;

		public OrderItemFactory()
		{
			_count = 2;
			_name = "laptop";
		}


		public OrderItem GetSomeOrderItem()
		{
			return new OrderItem(_count, _name);
		}

		public OrderItem GetSomeOrderItem(int count)
		{
			return new OrderItem(count, _name);
		}

		public OrderItem GetSomeOrderItem(string name)
		{
			return new OrderItem(_count, name);
		}

		public OrderItem GetSomeOrderItem(int count, string name)
		{
			return new OrderItem(count, name);
		}
	}
}

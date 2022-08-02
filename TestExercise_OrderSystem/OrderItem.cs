using OrderSystem.BusinessExceptions;

namespace OrderSystem
{
	public class OrderItem
	{
		public int Count { get; private set; }
		public string Name { get; private set; }

		public OrderItem(int count, string name)
		{
			GuardAgainstInvalidCount(count);

			Count = count;
			Name = name;
		}


		private static void GuardAgainstInvalidCount(int count)
		{
			if (count <= 0 || count > 3)
				throw new InvalidCountException();
		}
	}
}
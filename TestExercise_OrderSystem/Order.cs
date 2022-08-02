using OrderSystem.BusinessExceptions;

namespace OrderSystem
{
	public class Order
	{
		public int UserId { get; private set; }
		public OrderState State { get; private set; }
		public List<OrderItem> OrderItems { get; private set; } = new();

		public Order(int userId, OrderItem orderItem)
		{
			GuardAgainstInvalidOrderItem(orderItem);

			UserId = userId;
			State = OrderState.Created;
			OrderItems.Add(orderItem);
		}


		private static void GuardAgainstInvalidOrderItem(OrderItem orderItem)
		{
			if (orderItem == null)
				throw new NullOrderItemException();
		}

		public void SetOrderStateToFinalized()
		{
			if (State == OrderState.shipped)
				throw new SetStateToFinalizedException();

			State = OrderState.Finalized;
		}

		public void SetOrderStateToShipped()
		{
			if (State == OrderState.Created)
				throw new SetStateToShippedException();

			State = OrderState.shipped;
		}

		public void AddOrderItem(OrderItem orderItem)
		{
			GuardAgainstInvalidOrderItem(orderItem);

			if (State != OrderState.Created)
				throw new InvalidOrderStateException();

			OrderItems.Add(orderItem);
		}
	}


	public enum OrderState
	{
		Created = 1,
		Finalized = 2,
		shipped = 3
	}
}

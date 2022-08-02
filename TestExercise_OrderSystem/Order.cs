using OrderSystem.BusinessExceptions;

namespace OrderSystem
{
	public class Order
	{
		public int UserId { get; private set; }
		public OrderState State { get; private set; }
		public List<OrderItem> OrderItems { get; private set; } = new();

		public Order(int userId, List<OrderItem> orderItems)
		{
			GuardAgainstInvalidOrderItem(orderItems);

			UserId = userId;
			State = OrderState.Created;
			OrderItems.AddRange(orderItems);
		}


		private static void GuardAgainstInvalidOrderItem(List<OrderItem> orderItems)
		{
			if (orderItems == null || orderItems.Count == 0)
				throw new NullOrEmptyOrderItemsException();
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
			if (orderItem is null)
				throw new NullOrderItemException();

			if (State != OrderState.Created)
				throw new InvalidOrderStateException();

			OrderItems.Add(orderItem);
		}

		public void RemoveOrderItem(OrderItem orderItem)
		{
			if (State != OrderState.Created)
				throw new InvalidOrderStateException();

			if (OrderItems.Count == 1)
				throw new NullOrEmptyOrderItemsException();

			OrderItems.Remove(orderItem);
		}
	}


	public enum OrderState
	{
		Created = 1,
		Finalized = 2,
		shipped = 3
	}
}

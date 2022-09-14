using OrderSystem.BusinessExceptions;
using System.Collections.ObjectModel;

namespace OrderSystem
{
	public class Order
	{
		
		private readonly List<OrderItem> _orderItems = new();
		private readonly IMessageService _messageService;
		private readonly IOrderRepository _orderRepository;
		
        public Order(int userId, List<OrderItem> orderItems, IMessageService messageService, IOrderRepository orderRepository)
		{
			GuardAgainstInvalidOrderItem(orderItems);

			UserId = userId;
			State = OrderState.Created;
			_orderItems.AddRange(orderItems);
			_messageService = messageService;
			_orderRepository = orderRepository;

			NotifyUser();
		}

        public int UserId { get; private set; }
        public OrderState State { get; private set; }
        public IEnumerable<OrderItem> OrderItems => _orderItems.AsReadOnly();


        private static void GuardAgainstInvalidOrderItem(List<OrderItem> orderItems)
		{
			if (orderItems == null || orderItems.Count == 0)
				throw new NullOrEmptyOrderItemsException();
		}

		private void NotifyUser()
		{
			const string message = "New order just submited.";
			const string phoneNumber = "0912";

			_messageService.Send(message, phoneNumber);
		}

		public void Finalized()
		{
			if (State == OrderState.shipped)
				throw new SetStateToFinalizedException();

			State = OrderState.Finalized;
		}

		public void Shipped()
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

			_orderItems.Add(orderItem);
		}

		public void RemoveOrderItem(OrderItem orderItem)
		{
			if (State != OrderState.Created)
				throw new InvalidOrderStateException();

			if (OrderItems.Count() == 1)
				throw new NullOrEmptyOrderItemsException();

			_orderItems.Remove(orderItem);
		}

		public Order GetOrderById(int id)
		{
			var order = _orderRepository.GetById(id);
			
			if(order == null)
				throw new NullOrderException();

			return order;
		}
	}


	public enum OrderState
	{
		Created = 1,
		Finalized = 2,
		shipped = 3
	}
}

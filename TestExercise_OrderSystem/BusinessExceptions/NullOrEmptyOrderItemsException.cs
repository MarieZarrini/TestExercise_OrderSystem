namespace OrderSystem.BusinessExceptions
{
	public class NullOrEmptyOrderItemsException : Exception
	{
		public override string Message => "Order Items list cannot be empty or null.";
	}
}

namespace OrderSystem.BusinessExceptions
{
	public class NullOrderItemException : Exception
	{
		public override string Message => "Order Items list cannot be empty or null.";
	}
}

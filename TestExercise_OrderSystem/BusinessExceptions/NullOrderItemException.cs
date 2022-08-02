namespace OrderSystem.BusinessExceptions
{
	public class NullOrderItemException : Exception
	{
		public override string Message => "Order Item cannot be null";
	}
}
